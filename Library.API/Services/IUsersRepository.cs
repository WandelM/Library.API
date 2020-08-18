using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IUsersRepository : IRepository<LibraryUser>
    {
        Task<LibraryUser> GetLibraryUserWithCard(Guid usersId);
        bool IsUserUnique(LibraryUser user);
    }
}
