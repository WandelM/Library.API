using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("/api/publications")]
    public class PublicationController:ControllerBase
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IPublicationRepository publicationRepository, IMapper mapper)
        {
            _publicationRepository = publicationRepository ??
                throw new NullReferenceException(nameof(authorRepository));

            _mapper = mapper ??
                throw new NullReferenceException(nameof(mapper));
        }
    }
}

