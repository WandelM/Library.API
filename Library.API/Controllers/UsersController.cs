using AutoMapper;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("/api/users")]
    [Produces("application/json")]
    public class UsersController:ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository ?? throw new NullReferenceException(typeof(IUsersRepository).ToString());
            _mapper = mapper ?? throw new NullReferenceException(typeof(IMapper).ToString());
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<Dtos.UserOutputModel>> GetUser(Guid userId)
        {
            var user = await _usersRepository.GetAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userOutput = _mapper.Map<Dtos.UserOutputModel>(user);

            return Ok(userOutput);
        }

        [HttpPost]
        public async Task<ActionResult<Dtos.UserOutputModel>> AddNewUser(Dtos.UserInputModel userInput)
        {
                if (userInput == null)
                {
                    return BadRequest();
                }

                var userToInsert = _mapper.Map<Domain.Models.LibraryUser>(userInput);

            if (!_usersRepository.IsUserUnique(userToInsert))
            {
                return BadRequest("User already exists.");
            }

                _usersRepository.Add(userToInsert);
                await _usersRepository.SaveChangesAsync();

                var userOutput = _mapper.Map<Dtos.UserOutputModel>(userToInsert);

                return CreatedAtRoute("GetUser", new { userId = userToInsert.Id }, userOutput); 
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid usersId)
        {
            var user = await _usersRepository.GetAsync(usersId);

            if (user == null)
            {
                return NotFound();
            }

            _usersRepository.Remove(usersId);
            await _usersRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(Dtos.UserUpdateModel updateUser)
        {
            var userFromRepo = await _usersRepository.GetAsync(updateUser.Id);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(updateUser, userFromRepo);

            await _usersRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
