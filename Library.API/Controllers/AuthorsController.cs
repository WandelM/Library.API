﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{authorId}", Name ="GetAuthor")]
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
    }
}