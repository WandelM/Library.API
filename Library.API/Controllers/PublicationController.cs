using AutoMapper;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("/api/publications")]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoriesRepository _categoryRepository;
        private readonly IPublicationHouseRepository _publicationHouseRepository;
        private readonly IMapper _mapper;

        public PublicationController(IPublicationRepository publicationRepository, IMapper mapper,
            IAuthorRepository authorRepository, ICategoriesRepository categoriesRepository,
            IPublicationHouseRepository publicationHouseRepository)
        {
            _publicationRepository = publicationRepository ??
                throw new NullReferenceException(nameof(publicationRepository));

            _mapper = mapper ??
                throw new NullReferenceException(nameof(mapper));

            _authorRepository = authorRepository ??
                throw new NullReferenceException(nameof(authorRepository));

            _categoryRepository = categoriesRepository ??
                throw new NullReferenceException(nameof(categoriesRepository));

            _publicationHouseRepository = publicationHouseRepository ??
                throw new NullReferenceException(nameof(publicationHouseRepository));
        }

        [HttpGet("{publicationId}", Name = "GetPublication")]
        [Produces("application/json")]
        public async Task<ActionResult<Dtos.PublicationOutputModel>> GetPublication(Guid publicationId)
        {
            var publicationFromRepo = await _publicationRepository.GetFullPublicationAsync(publicationId);

            if (publicationFromRepo == null)
            {
                return NotFound();
            }

            var authorsFromPublication = publicationFromRepo.PublicationAuthors.Select(pa => pa.Author);
            var categoriesFromPublication = publicationFromRepo.PublicationCategories.Select(pc => pc.Category);

            var authorsToReturn = _mapper.Map<ICollection<Dtos.AuthorOutputModel>>
                (authorsFromPublication);

            var categoriesToReturn = _mapper.Map<ICollection<Dtos.CategoryOutputModel>>(categoriesFromPublication);

            var publicationToReturn = _mapper.Map<Dtos.PublicationOutputModel>(publicationFromRepo);

            publicationToReturn.Categories = categoriesToReturn;
            publicationToReturn.Authors = authorsToReturn;

            return Ok(publicationToReturn);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Dtos.PublicationOutputModel>>> GetAllPublications()
        {
            var publicationsFromRepo = await _publicationRepository.GetFullPublicationsAsync();

            if (publicationsFromRepo == null)
            {
                return NotFound();
            }

            var publicationsToReturn = _mapper.Map<IEnumerable<Dtos.PublicationOutputModel>>(publicationsFromRepo);

            foreach (var publication in publicationsToReturn)
            {
                publication.Authors = publicationsFromRepo.Single(p => p.Id == publication.Id)
                    .PublicationAuthors.Select(pa => _mapper.Map<Dtos.AuthorOutputModel>(pa.Author));

                publication.Categories = publicationsFromRepo.Single(p => p.Id == publication.Id)
                    .PublicationCategories.Select(pc => _mapper.Map<Dtos.CategoryOutputModel>(pc.Category));
            }

            return Ok(publicationsToReturn);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Dtos.PublicationOutputModel>> AddPublication(
            Dtos.PublicationInputModel publicationInputModel)
        {
            var publicationToInsert = _mapper.Map<Domain.Models.Publication>(publicationInputModel);

            publicationToInsert.Id = Guid.NewGuid();

            var authorIdsFromInput = publicationInputModel.AuthorIds;

            var publicationAuthors = new List<Domain.Models.PublicationAuthors>();

            foreach (var authorId in authorIdsFromInput)
            {
                var authorFromRepo = await _authorRepository.GetAsync(authorId);

                if (authorFromRepo == null)
                {
                    return NotFound($"Author with id: {authorId} was not found");
                }

                var publicationAuthor = new Domain.Models.PublicationAuthors()
                {
                    Author = authorFromRepo,
                    AuthorId = authorFromRepo.Id,
                    Publication = publicationToInsert,
                    PublicationId = publicationToInsert.Id
                };

                publicationAuthors.Add(publicationAuthor);
            }

            var categoryIdsFromInput = publicationInputModel.CategoryIds;

            var publicationCategories = new List<Domain.Models.PublicationCategories>();

            foreach (var categoryId in categoryIdsFromInput)
            {
                var categoryFromRepo = await _categoryRepository.GetAsync(categoryId);

                if (categoryFromRepo == null)
                {
                    return NotFound($"Category with id: {categoryId} was not found");
                }

                var publicationCategory = new Domain.Models.PublicationCategories()
                {
                    Category = categoryFromRepo,
                    CategoryId = categoryId,
                    Publication = publicationToInsert,
                    PublicationId = publicationToInsert.Id
                };

                publicationCategories.Add(publicationCategory);
            }

            var publicationHouseFromInput = await _publicationHouseRepository
                .GetAsync(publicationInputModel.PublicationHouseId);

            if (publicationHouseFromInput == null)
            {
                return NotFound($"Publication House with id: {publicationHouseFromInput.Id} was not found");
            }

            publicationToInsert.PublicationAuthors = publicationAuthors;
            publicationToInsert.PublicationCategories = publicationCategories;
            publicationToInsert.PublicationHouseId = publicationHouseFromInput.Id;

            _publicationRepository.Add(publicationToInsert);
            await _publicationRepository.SaveChangesAsync();

            var publicationOutput = await GetPublication(publicationToInsert.Id);

            return CreatedAtRoute("GetPublication", new { publicationId = publicationToInsert.Id }
            , publicationOutput.Result);
        }

        [HttpDelete("{publicationId}")]
        public async Task<IActionResult> DeletePublication(Guid publicationId)
        {
            var publicationToDelete = await _publicationRepository.GetFullPublicationAsync(publicationId);

            if (publicationToDelete == null)
            {
                return NotFound();
            }

            _publicationRepository.Remove(publicationToDelete);
            await _publicationRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}

