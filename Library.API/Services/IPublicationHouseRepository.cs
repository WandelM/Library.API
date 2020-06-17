using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IPublicationHouseRepository:IRepository<Domain.Models.PublicationHouse>
    {
        void UpdatePublicationHouse(Domain.Models.PublicationHouse publicationHouseUpdateModel);
    }
}
