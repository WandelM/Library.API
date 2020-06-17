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
    public class AuthorsRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorsRepository(LibraryContext context):base(context)
        {

        }

        public void UpdateAuthor(Guid authorId, Author updatedAuthor)
        {
            if (updatedAuthor == null)
            {
                throw new NullReferenceException(nameof(updatedAuthor));
            }

            var authorToUpdate = _context.Set<Author>().Single(a => a.Id == authorId);

            authorToUpdate = updatedAuthor;
        }

        public bool AuthorExists(Guid authorId)
        {
            return _context.Set<Author>().Any(a => a.Id == authorId);
        }

        public async Task<Author> GetAuthorAsync(Guid authorId)
        {
            var author = await _context.Set<Author>().FirstOrDefaultAsync(a => a.Id == authorId);

            return author;
        }
    }
}
