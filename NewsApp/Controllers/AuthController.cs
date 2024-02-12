using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsApp.Data;
using NewsApp.Dto;
using NewsApp.Interface;
using NewsApp.Models;
using NewsApp.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace NewsApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary> Регистрация пользователя </summary>
        [HttpPost("singup")]
        [ProducesResponseType(200, Type = typeof(AuthOutputDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] SingupDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null)
                return BadRequest(ModelState);

            var user = await _userRepository.GetUserAsync(request.Email);

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            return Ok();
        }

        /// <summary> Аутентификация пользователя </summary>
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(AuthOutputDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (request == null)
                return BadRequest(ModelState);

            var user = await _userRepository.GetUserAsync(request.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User does not exists");
                return StatusCode(422, ModelState);
            }

            return Ok();
        }

        /// <summary> Проверить токен </summary>
        [HttpGet("whoami"), Authorize]
        [ProducesResponseType(200, Type = typeof(PublicUserDto))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetMe()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail == null)
            { return BadRequest(ModelState); }

            var user = _mapper.Map<PublicUserDto>(await _userRepository.GetUserAsync(userEmail));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

    }
}
