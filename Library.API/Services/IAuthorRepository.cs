using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IAuthorRepository:IRepository<Author>
    {
        Task<Author> GetAuthorAsync(Guid authorId);
        bool AuthorExists(Guid authorId);
        void Delete(Guid id);
        void UpdateAuthor(Guid authorId, Author updatedAuthor);
    }
}
