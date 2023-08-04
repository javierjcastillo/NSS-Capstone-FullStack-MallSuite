using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using MallSuite.Models;
using MallSuite.Repositories;

namespace MallSuite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetUser(string firebaseUserId)
        {
            return Ok(_userRepository.GetByFirebaseUserId(firebaseUserId));
        }

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var userProfile = _userRepository.GetByFirebaseUserId(firebaseUserId);

            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        [HttpGet("details/{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            _userRepository.Add(user);
            return CreatedAtAction(
            nameof(GetUser),
            new { firebaseUserId = user.FirebaseUserId }, user);
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            _userRepository.Delete(userId);
            return NoContent();
        }
    }
}
