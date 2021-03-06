﻿using AutoMapper;
using Library.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.MappingProfiles
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorInputModel, Domain.Models.Author>();
            CreateMap<Domain.Models.Author, AuthorOutputModel>();
            CreateMap<AuthorForUpdateModel, Domain.Models.Author>();
            CreateMap<Domain.Models.Author, AuthorForUpdateModel>();
        }
    }
}
