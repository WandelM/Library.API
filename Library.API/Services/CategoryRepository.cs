using Library.API.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class CategoryRepository:RepositoryBase<Domain.Models.Category>, ICategoriesRepository
    {
        public CategoryRepository(LibraryContext context):base(context)
        {
        }
    }
}
