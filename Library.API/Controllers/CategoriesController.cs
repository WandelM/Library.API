using AutoMapper;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesController:ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryOutputModel>>> GetAllCategories()
        {
            var categoriesFromRepo = await _categoriesRepository.GetAllAsync();

            if (categoriesFromRepo.Count() == 0)
            {
                return NotFound();
            }

            var mappedCategories = _mapper.Map<IEnumerable<CategoryOutputModel>>(categoriesFromRepo);

            return Ok(mappedCategories);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryOutputModel>> GetCategorie(Guid categoryId)
        {
            var categoryFromRepo = await _categoriesRepository.GetAsync(categoryId);

            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            var mappedCategory = _mapper.Map<CategoryOutputModel>(categoryFromRepo);

            return Ok(mappedCategory);
        }
    }
}
