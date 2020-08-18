using Library.API.DbContexts;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class UsersRepository : RepositoryBase<LibraryUser>, IUsersRepository
    {
        public UsersRepository(LibraryContext context) : base(context)
        {
        }

        public Task<LibraryUser> GetLibraryUserWithCard(Guid usersId)
        {
            throw new NotImplementedException();
        }

        public bool IsUserUnique(LibraryUser user)
        {
            var foundUser = _context.Set<LibraryUser>().FirstOrDefault(lu => lu.Name == user.Name 
            && lu.Surname == user.Surname && lu.BirthDate == user.BirthDate);

            if (foundUser == null)
            {
                return true;
            }

            return false;
        }
    }
}
