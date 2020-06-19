using Library.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class PublicationRepository : RepositoryBase<Domain.Models.Publication>, IPublicationRepository
    {
        public DbContext _libraryContext;

        public PublicationRepository(LibraryContext libraryContext):base(context:libraryContext)
        {
        }
    }
}
