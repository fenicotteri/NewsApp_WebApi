using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Dto;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IRatingRepository ratingRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _ratingRepository = ratingRepository;
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
        [ProducesResponseType(200, Type = typeof(PublicUserWithRatingDto))]
        [ProducesResponseType(400)]
        public ActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _mapper.Map<PublicUserWithRatingDto>(_userRepository.GetUser(id));
            user.Rating = _ratingRepository.GetValueUserRating(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

    } 

    
}
