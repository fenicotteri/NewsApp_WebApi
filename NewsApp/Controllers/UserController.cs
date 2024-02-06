using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Dto;
using NewsApp.Interface;
using NewsApp.Models;
using System.Security.Claims;

namespace NewsApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper; 
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PublicUserDto>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<PublicUserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PublicUserDto))]
        [ProducesResponseType(400)]
        public ActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _mapper.Map<PublicUserDto>(_userRepository.GetUser(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPatch("{id}"), Authorize]
        public async Task<IActionResult> UpdateUserPatch([FromBody] UpdateUserDto request, int id)
        {
            if (request == null)
                return BadRequest(ModelState);

            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                ModelState.AddModelError("", "User does not exists");
                return StatusCode(404, ModelState);
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if(userEmail != user.Email)
            {
                ModelState.AddModelError("", "Wrong password or email");
                return StatusCode(403, ModelState);
            }

            var patchDocument = new JsonPatchDocument<User>();
            patchDocument.Replace(x => x.LastName, request.LastName);
            patchDocument.Replace(x => x.FirstName, request.FirstName);

            if (!await _userRepository.UpdateUserPatchAsync(user, patchDocument))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    } 

    
}
