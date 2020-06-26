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
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Dtos.PublicationHouseOutputModel>>> GetAll()
        {
            var publicationHousesList = await _publicationHouseRepository.GetAllAsync();

            if (publicationHousesList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<Dtos.PublicationHouseOutputModel>>(publicationHousesList));
        }

        [HttpGet("{publicationHouseId}", Name = "GetPublicationHouse")]
        [Produces("application/json")]
        public async Task<ActionResult<Dtos.PublicationHouseOutputModel>> GetPublicationHouse(Guid publicationHouseId)
        {
            var publicationHouse = await _publicationHouseRepository.GetAsync(publicationHouseId);

            if (publicationHouse == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Dtos.PublicationHouseOutputModel>(publicationHouse));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Dtos.PublicationHouseOutputModel>> AddPublicationHouse(
            Dtos.PublicationHouseInputModel publicationHouseInput)
        {
            var mappedPublicationHouse = _mapper.Map<Domain.Models.PublicationHouse>(publicationHouseInput);

            _publicationHouseRepository.Add(mappedPublicationHouse);
            await _publicationHouseRepository.SaveChangesAsync();

            var publicationHouseOutput = _mapper.Map<Dtos.PublicationHouseOutputModel>(mappedPublicationHouse);

            return CreatedAtRoute("GetPublicationHouse", new { publicationHouseId = mappedPublicationHouse.Id },
                publicationHouseOutput);
        }
    }
}
