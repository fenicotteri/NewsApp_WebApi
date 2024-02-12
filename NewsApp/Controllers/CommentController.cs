using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Dto;
using NewsApp.Interface;
using NewsApp.Models;
using NewsApp.Repository;
using System.Security.Claims;

namespace NewsApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        // Объект CommentRepository - Реализация ICommentRepository
        // Этот сервис ответственен за выполнение операций с комментариями
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository,IPostRepository postRepository, IMapper mapper)
        {
            // экземпляры необходимых сервисов
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        /// <summary> Получить список комментариев </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentOutputDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCommentsAsync()
        {
            var comments = _mapper.Map<List<CommentOutputDto>>(await _commentRepository.GetCommentsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(comments);

        }

        /// <summary> Создать комментарий </summary>
        [HttpPost, Authorize]
        [ProducesResponseType(200, Type = typeof(CommentOutputDto))]
        [ProducesResponseType(400)]
        async public Task<IActionResult> addCommentAsync([FromBody] CreateCommentDto request)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
           
            if (request == null)
                return BadRequest(ModelState);

            var post = await _postRepository.GetPostAsync(request.PostId);

            if (post == null)
            {
                ModelState.AddModelError("", "Post does not exists");
                return StatusCode(404, ModelState);
            }

            var commentMap = _mapper.Map<Comment>(request);

            if (!await _commentRepository.CreateCommentAsync(userEmail, request.PostId, commentMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            var outputComment = _mapper.Map<CommentOutputDto>(commentMap);
            return Ok(outputComment);
        }

        /// <summary> Удалить комментарий </summary>
        [HttpDelete("{id:int}"), Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        async public Task<IActionResult> DeletePostAsync(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var commentToDelete = await _commentRepository.GetCommentAsync(id);

            if (commentToDelete == null)
            {
                return NotFound();
            }

            if (commentToDelete.Author != null && commentToDelete.Author.Email != userEmail)
            {
                ModelState.AddModelError("", "You are not an author");
                return StatusCode(403, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _commentRepository.DeletecommentAsync(commentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
