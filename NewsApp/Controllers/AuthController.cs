using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        // private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<User> userManager,ITokenService tokenService, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        /// <summary> Регистрация пользователя </summary>
        [HttpPost("singup")]
        [ProducesResponseType(200, Type = typeof(AuthOutputDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] SingupDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (request == null)
                    return BadRequest(ModelState);

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    ModelState.AddModelError("", "User already exists");
                    return StatusCode(422, ModelState);
                }

                var userMap = _mapper.Map<User>(request);
                userMap.UserName = userMap.Email;
                var createdUser = await _userManager.CreateAsync(userMap, request.Password);

                if (createdUser.Succeeded)
                { 
                    return Ok(
                        new AuthOutputDto
                        {
                            User = _mapper.Map<PublicUserDto>(await _userManager.FindByEmailAsync(request.Email)),
                            AccessToken = _tokenService.CreateToken(userMap)
                        }
                    );

                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        /// <summary> Аутентификация пользователя </summary>
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(AuthOutputDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (request == null)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized("Invalid Email");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            return Ok(  
                        new AuthOutputDto
                        {
                            User = _mapper.Map<PublicUserDto>(await _userManager.FindByEmailAsync(request.Email)),
                            AccessToken = _tokenService.CreateToken(user)
                        }
                    );
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

            var user = _mapper.Map<PublicUserDto>(await _userManager.FindByEmailAsync(userEmail));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

    }
}
