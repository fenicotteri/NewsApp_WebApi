﻿using AutoMapper;
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
        private readonly IUserRepository _userRepository ;
        private readonly IMapper _mapper ;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = configuration;
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

            var userMap = _mapper.Map<User>(request);
            userMap.UpdatedAt = DateTime.Now;

            CreatePasswordHash(request.Password, userMap);

            if (! await _userRepository.CreateUserAsync(userMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            AuthOutputDto result = new AuthOutputDto();
            result.User = _mapper.Map<PublicUserDto>(await _userRepository.GetUserAsync(request.Email));
            result.AccessToken = CreateToken(userMap);

            return Ok(result);
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

            if (!VerifyPasswordHash(request.Password, user)) 
                return BadRequest(ModelState);

            AuthOutputDto result = new AuthOutputDto();
            result.User = _mapper.Map<PublicUserDto>(await _userRepository.GetUserAsync(request.Email));
            result.AccessToken = CreateToken(user) ;

            return Ok(result);
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

        private void CreatePasswordHash(string password, User user ) 
        {
            using (var hmac = new HMACSHA512())
            { 
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, User user)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(user.PasswordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                ); 

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
