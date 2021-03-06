﻿using AutoMapper;
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
            CreateMap<Dtos.PublicationHouseUpdateModel, Domain.Models.PublicationHouse>();
            CreateMap<Domain.Models.PublicationHouse, Dtos.PublicationHouseOutputModel>();
            CreateMap<Dtos.PublicationHouseInputModel, Domain.Models.PublicationHouse>();
        }
    }
}
