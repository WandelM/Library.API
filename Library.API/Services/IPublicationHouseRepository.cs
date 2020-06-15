using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    interface IPublicationHouseRepository:IRepository<Domain.Models.PublicationHouse>
    {
        Task<Domain.Models.PublicationHouse> GetPublicationHouseAsync(Guid id);
        bool DeletePublicationHouse(Guid publicationHouseId);
        void UpdatePublicationHouse(Domain.Models.PublicationHouse publicationHouseUpdateModel);
    }
}
