using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/authors")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorOutputModel>>> GetAuthors()
        {
            var authorsFromDb = await _authorRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<AuthorOutputModel>>(authorsFromDb));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
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

            _authorRepository.Delete(authorToDelete);
            await _authorRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{authorId}")]
        public async Task<ActionResult<AuthorOutputModel>> PartiallyUpdateAuthor(Guid authorId, 
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
    }
}