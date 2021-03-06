﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IPublicationRepository:IRepository<Domain.Models.Publication>
    {
        Task<Domain.Models.Publication> GetFullPublicationAsync(Guid publicationId);
        Task<ICollection<Domain.Models.Publication>> GetFullPublicationsAsync();
    }
}
