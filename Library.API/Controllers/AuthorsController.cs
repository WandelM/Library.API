using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Dtos;
using Library.API.Helpers;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    [Produces("application/json")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository ??
                throw new NullReferenceException(nameof(authorRepository));

            _mapper = mapper ??
                throw new NullReferenceException(nameof(mapper));
        }

        [HttpGet(Name = "GetAuthors")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<AuthorOutputModel>>> GetAuthors(int pageNumber, int pageSize)
        {
            var authorsFromDb = await _authorRepository.GetPaginatedListAsync(pageSize, pageNumber);

            var nextPageLink = authorsFromDb.HasNext ? 
                GeneratePaginationLinks(LinkType.Next, pageNumber, pageSize) : null;

            var previousPageLink = authorsFromDb.HasPrevious ? 
                GeneratePaginationLinks(LinkType.Previous, pageNumber, pageSize) : null;

            var currentPageLink = GeneratePaginationLinks(LinkType.Current, pageNumber, pageSize);

            var paginationMetadata = new PaginationMetaData()
            {
                CurrentPageLink = currentPageLink,
                NextPageLink = nextPageLink,
                PreviousPageLink = previousPageLink,
                TotalCount = authorsFromDb.TotalCount,
                TotalPages = authorsFromDb.TotalPages
            };

            Response.Headers.Add("PaginationMetadata", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<AuthorOutputModel>>(authorsFromDb));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        [Produces("application/json")]
        public async Task<ActionResult<AuthorOutputModel>> GetAuthor(Guid authorId)
        {
            var authorFromDb = await _authorRepository.GetAuthorAsync(authorId);

            if (authorFromDb == null)
            {
                return NotFound();
            }

            var authorToReturn = _mapper.Map<AuthorOutputModel>(authorFromDb);

            return Ok(authorToReturn);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<AuthorOutputModel>> AddAuthor(AuthorInputModel authorInput)
        {
            if (authorInput == null)
            {
                throw new NullReferenceException(nameof(authorInput));
            }

            var authorToInsert = _mapper.Map<Domain.Models.Author>(authorInput);

            _authorRepository.Add(authorToInsert);
            await _authorRepository.SaveChangesAsync();

            var authorToReturn = _mapper.Map<AuthorOutputModel>(authorToInsert);

            return CreatedAtRoute("GetAuthor", new { authorId = authorToInsert.Id }, authorToReturn);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorOutputModel>> UpdateAuthor(AuthorForUpdateModel authorToUpdate)
        {
            var authorExists = _authorRepository.AuthorExists(authorToUpdate.Id);

            if (!authorExists)
            {
                return NotFound();
            }

            var updatedAuthor = _mapper.Map<Domain.Models.Author>(authorToUpdate);

            _authorRepository.UpdateAuthor(authorToUpdate.Id, updatedAuthor);
            await _authorRepository.SaveChangesAsync();

            var authorOutput = _mapper.Map<AuthorOutputModel>(updatedAuthor);

            return Ok(authorOutput);
        }

        [HttpDelete("{authorId}")]
        public async Task<IActionResult> DeleteAuthor(Guid authorId)
        {
            var authorToDelete = await _authorRepository.GetAuthorAsync(authorId);

            if (authorToDelete == null)
            {
                return NotFound();
            }

            _authorRepository.Remove(authorToDelete.Id);
            await _authorRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{authorId}")]
        public async Task<IActionResult> PartiallyUpdateAuthor(Guid authorId, 
            JsonPatchDocument<AuthorForUpdateModel> patchDocument)
        {
            var authorFromDb = await _authorRepository.GetAuthorAsync(authorId);

            if (authorFromDb == null)
            {
                return NotFound();
            }

            var authorForUpdate = _mapper.Map<AuthorForUpdateModel>(authorFromDb);

            patchDocument.ApplyTo(authorForUpdate, ModelState);

            if (!TryValidateModel(authorForUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(authorForUpdate, authorFromDb);

            _authorRepository.UpdateAuthor(authorId, authorFromDb);

            await _authorRepository.SaveChangesAsync();

            return NoContent();
        }

        private string GeneratePaginationLinks(LinkType type, int pageNumber, int pageSize)
        {
            var output = string.Empty;

            switch (type)
            {
                case LinkType.Next:
                    output = Url.Link("GetAuthors", new { pageNumber = pageNumber + 1, pageSize });
                    break;
                case LinkType.Previous:
                    output = Url.Link("GetAuthors", new { pageNumber = pageNumber -1 , pageSize });
                    break;
                case LinkType.Current:
                    output = Url.Link("GetAuthors", new { pageNumber, pageSize });
                    break;
                default:
                    break;
            }

            return output;
        }

        private class PaginationMetaData
        {
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public string CurrentPageLink { get; set; }
            public string NextPageLink { get; set; }
            public string PreviousPageLink { get; set; }
        }
    }
}