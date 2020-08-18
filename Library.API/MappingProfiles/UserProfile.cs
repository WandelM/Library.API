using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.MappingProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Domain.Models.LibraryUser, Dtos.UserOutputModel>();
            CreateMap<Dtos.UserInputModel, Domain.Models.LibraryUser>();
            CreateMap<Dtos.UserUpdateModel, Domain.Models.LibraryUser>();
        }
    }
}
