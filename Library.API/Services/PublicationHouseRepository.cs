using Library.API.DbContexts;
using Library.API.Models;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class PublicationHouseRepository : RepositoryBase<PublicationHouse>, IPublicationHouseRepository
    {
        public PublicationHouseRepository(LibraryContext context):base(context)
        {

        }

        public void UpdatePublicationHouse(Domain.Models.PublicationHouse publicationHouseUpdateModel)
        {
            if (publicationHouseUpdateModel == null)
            {
                throw new ArgumentNullException(nameof(publicationHouseUpdateModel));
            }

            var publicationHouseToUpdate = _context.Set<PublicationHouse>()
                .FirstOrDefault(ph => ph.Id == publicationHouseUpdateModel.Id);

            publicationHouseToUpdate = publicationHouseUpdateModel;
        }
    }
}
