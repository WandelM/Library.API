using Library.API.DbContexts;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Library.API.Services
{
    public class AuthorsRepository : IAuthorRepository
    {
        private readonly LibraryContext _libraryContext;
        

        public AuthorsRepository(LibraryContext libraryContex)
        {
            _libraryContext = libraryContex ??
                throw new NullReferenceException(nameof(libraryContex));
        }

        public void Add(Author value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }

            _libraryContext.Authors.Add(value);
        }

        public void Delete(Author value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }

            var authorExits = _libraryContext.Authors.Any(a => a == value);

            if (!authorExits)
            {
                throw new Exception("Author does not exists");
            }

            _libraryContext.Authors.Remove(value);
        }

        public void Delete(Guid authorId)
        {
            var authorToDelete = _libraryContext.Authors.Single(a => a.Id == authorId);

            _libraryContext.Authors.Remove(authorToDelete);
        }

        public async Task SaveChangesAsync()
        {
            var hasChanges = _libraryContext.ChangeTracker.HasChanges();

            if (hasChanges)
            {
                await _libraryContext.SaveChangesAsync();
            }
        }

        public void UpdateAuthor(Guid authorId, Author updatedAuthor)
        {
            if (updatedAuthor == null)
            {
                throw new NullReferenceException(nameof(updatedAuthor));
            }

            var authorToUpdate = _libraryContext.Authors.Single(a => a.Id == authorId);

            authorToUpdate = updatedAuthor;
        }

        public bool AuthorExists(Guid authorId)
        {
            return _libraryContext.Authors.Any(a => a.Id == authorId);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var authorsList = await _libraryContext.Authors.ToListAsync();
            
            return authorsList;
        }

        public async Task<Author> GetAuthorAsync(Guid authorId)
        {
            var author = await _libraryContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId);

            return author;
        }
    }
}
