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
    public class PublicationHouseRepository : IPublicationHouseRepository
    {
        private readonly LibraryContext _libraryContext;

        public PublicationHouseRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext ??
                throw new ArgumentNullException(nameof(libraryContext));
        }

        public void Add(PublicationHouse value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _libraryContext.PublicationHouses.Add(value);
        }

        public bool DeletePublicationHouse(Guid publicationHouseId)
        {
            var publicationHouseToDelete = _libraryContext.PublicationHouses.FirstOrDefault(ph => ph.Id == publicationHouseId);

            if (publicationHouseId == null)
            {
                return false;
            }

            _libraryContext.PublicationHouses.Remove(publicationHouseToDelete);
            return true;
        }

        public async Task<IEnumerable<PublicationHouse>> GetAllAsync()
        {
            var publicationHouses = await _libraryContext.PublicationHouses.ToListAsync();
            
            return publicationHouses;
        }

        public async Task<Domain.Models.PublicationHouse> GetPublicationHouseAsync(Guid id)
        {
            var publicationHouse = await _libraryContext.PublicationHouses.FirstOrDefaultAsync(ph => ph.Id == id);

            return publicationHouse;
        }

        public async Task SaveChangesAsync()
        {
            var hasChanges = _libraryContext.ChangeTracker.HasChanges();

            if (hasChanges)
            {
                await _libraryContext.SaveChangesAsync();
            }
        }

        public void UpdatePublicationHouse(Domain.Models.PublicationHouse publicationHouseUpdateModel)
        {
            if (publicationHouseUpdateModel == null)
            {
                throw new ArgumentNullException(nameof(publicationHouseUpdateModel));
            }

            var publicationHouseToUpdate = _libraryContext.PublicationHouses
                .FirstOrDefault(ph => ph.Id == publicationHouseUpdateModel.Id);

            publicationHouseToUpdate = publicationHouseUpdateModel;
        }
    }
}
