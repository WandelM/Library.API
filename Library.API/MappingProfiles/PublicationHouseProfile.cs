using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.MappingProfiles
{
    public class PublicationHouseProfile:Profile
    {
        public PublicationHouseProfile()
        {
            CreateMap<Models.PublicationHouseUpdateModel, Domain.Models.PublicationHouse>();
            CreateMap<Domain.Models.PublicationHouse, Models.PublicationHouseOutputModel>();
            CreateMap<Models.PublicationHouseInputModel, Domain.Models.PublicationHouse>();
        }
    }
}
