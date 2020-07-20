using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.MappingProfiles
{
    public class PublicationProfile:Profile
    {
        public PublicationProfile()
        {
            CreateMap<Domain.Models.Publication, Dtos.PublicationOutputModel>();
            CreateMap<Dtos.PublicationInputModel, Domain.Models.Publication>();
            CreateMap<Dtos.PublicationUpdateModel, Domain.Models.Publication>();
        }
    }
}
