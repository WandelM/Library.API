using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.MappingProfiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Domain.Models.Category, Dtos.CategoryOutputModel>();
            CreateMap<Dtos.CategoryInputModel, Domain.Models.Category>();
            CreateMap<Dtos.CategoryUpdateModel, Domain.Models.Category>();
        }
    }
}
