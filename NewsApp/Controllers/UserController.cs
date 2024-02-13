using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper; 
        }

        /// <summary> Получить список пользователей </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PublicUserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<List<PublicUserDto>>(await _userManager.Users.ToListAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        /// <summary> Получить пользователя по идентификатору </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PublicUserDto))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetUser(string id)
        {
            var user = _mapper.Map<PublicUserDto>(await _userManager.FindByIdAsync(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        
        /// <summary> Обновить данные пользователя </summary>
        [HttpPatch("{id}"), Authorize]
         [ProducesResponseType(200, Type = typeof(PublicUserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateUserPatch([FromForm] UpdateUserDto request, string id)
        {
            if (request == null)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(id);

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

            if (request.FileBin != null)
            {
                // Прочитать имя файла
                var fileName = Path.GetFileName(request.FileBin.FileName);

                // Прочитать содержимое файла в байтовый массив
                using (var memoryStream = new MemoryStream())
                {
                    await request.FileBin.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    patchDocument.Replace(x => x.FileBin, fileBytes);
                    patchDocument.Replace(x => x.FilePath, fileName);
                }
            
            }

            if (!await _userRepository.UpdateUserPatchAsync(user, patchDocument))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
        
    } 

    
}
