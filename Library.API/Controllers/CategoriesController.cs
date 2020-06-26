using AutoMapper;
using Library.API.Dtos;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
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

        [HttpGet("{categoryId}", Name = "GetCategory")]
        [Produces("application/json")]
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

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CategoryOutputModel>> CreateCategory(CategoryInputModel categoryInputModel)
        {
            var categoryToInsert = _mapper.Map<Domain.Models.Category>(categoryInputModel);

            _categoriesRepository.Add(categoryToInsert);
            await _categoriesRepository.SaveChangesAsync();

            var categoryOutput = _mapper.Map<Dtos.CategoryOutputModel>(categoryToInsert);

            return CreatedAtRoute("GetCategory", new { categoryId = categoryToInsert.Id }, categoryOutput);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var categoryToDelete = await _categoriesRepository.GetAsync(categoryId);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            _categoriesRepository.Remove(categoryToDelete);
            await _categoriesRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateModel categoryUpdateModel)
        {
            var categoryToUpdate = await _categoriesRepository.GetAsync(categoryUpdateModel.Id);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            categoryToUpdate = _mapper.Map<Domain.Models.Category>(categoryUpdateModel);
            
            await _categoriesRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
