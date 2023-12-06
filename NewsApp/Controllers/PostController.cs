using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Dto;
using NewsApp.Interface;
using NewsApp.Models;
using NewsApp.Repository;
using System.Collections.Generic;
using System.Security.Claims;

namespace NewsApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository postRepository,IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PostOutputDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPostsAsync()
        {
            var posts = _mapper.Map<List<PostOutputDto>>(await _postRepository.GetPostsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(posts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PostOutputDto))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetPostAsync(int id)
        {
            if (!_postRepository.PostExists(id))
                return NotFound();

            var post = _mapper.Map<PostOutputDto>(await _postRepository.GetPostAsync(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(post);
        }

        [HttpPost("{id}/tag"), Authorize]
        [ProducesResponseType(200, Type = typeof(TagOutputDto))]
        [ProducesResponseType(400)]
        async public Task<IActionResult> addTagAsync(int id, [FromBody] AddTagDto request)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
           
            if (request == null)
                return BadRequest(ModelState);

            var post = await _postRepository.GetPostAsync(id);

            if (post == null)
            {
                ModelState.AddModelError("", "Post does not exists");
                return StatusCode(404, ModelState);
            }

            if (post.Author.Email != userEmail)
            {
                ModelState.AddModelError("", "You are not an author");
                return StatusCode(403, ModelState);
            }

            var tagMap = _mapper.Map<Tag>(request);

            if (!await _postRepository.CreateTagAsync(id, tagMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            var outputTagMap = _mapper.Map<TagOutputDto>(tagMap);
            return Ok(outputTagMap);
        }

        [HttpDelete("{id}"), Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        async public Task<IActionResult> DeletePostAsync(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (!_postRepository.PostExists(id))
            {
                return NotFound();
            }

            var postToDelete = await _postRepository.GetPostAsync(id);

            if(postToDelete.Author.Email != userEmail)
            {
                ModelState.AddModelError("", "You are not an author");
                return StatusCode(403, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _postRepository.DeletePostAsync(postToDelete))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}/tag"), Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        async public Task<IActionResult> RemoveTagAsync(int id, [FromBody] RemoveTagDto request)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (!_postRepository.PostExists(id))
            {
                return NotFound();
            }

            var postTag = await _postRepository.GetPostAsync(id);

            if (postTag.Author.Email != userEmail)
            {
                ModelState.AddModelError("", "You are not an author");
                return StatusCode(403, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postTagForRemoving = await _postRepository.GetPostTagAsync(id, request.tagId);

            if (!await _postRepository.DeletePostTagAsync(postTagForRemoving))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        }
}
