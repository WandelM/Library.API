using AutoMapper;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController()]
    [Route("/api/publicationHouses")]
    public class PublicationHouseController:ControllerBase
    {
        private readonly IPublicationHouseRepository _publicationHouseRepository;
        private readonly IMapper _mapper;

        public PublicationHouseController(IPublicationHouseRepository publicationHouseRepository, IMapper mapper)
        {
            _publicationHouseRepository = publicationHouseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.PublicationHouseOutputModel>>> GetAll()
        {
            var publicationHousesList = await _publicationHouseRepository.GetAllAsync();

            if (publicationHousesList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<Models.PublicationHouseOutputModel>>(publicationHousesList));
        }
    }
}
