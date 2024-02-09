using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NewsApp.Data;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context) { 
            _context = context;
        }
        public async Task<Post> GetPostAsync(int id)
        {
            return await _context.Posts.Where(p => p.Id == id)
                .Include(a => a.Author)
                .Include(post => post.PostTags)
                    .ThenInclude(postTag => postTag.Tag)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPostsWithTagsAsync()
        {
            return await _context.Posts.OrderBy(x => x.Id)
                .Include(a => a.Author)
                .Include(t => t.PostTags)
                    .ThenInclude(postTag => postTag.Tag)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsAsync(string? order, string? search, string? author, int offset, int limit)
        {
            IQueryable<Post> response = _context.Posts.Include(a => a.Author);

            if (!string.IsNullOrWhiteSpace(search))
            {
                response = response.Where(x => x.Title.Contains(search) || x.Text.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                response = response.Where(x => x.Author.Email.Contains(author) || x.Author.FirstName.Contains(author) || x.Author.LastName.Contains(author));
            }

            if  (offset > 0 && limit > 0)
            {
                response = response.Skip(offset).Take(limit);
            }

            if (order == "desc")
            {
                response = response.OrderBy(x => x.CreatedAt);
            }

            return await response.ToListAsync();
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Where(p => p.Id == id).Any();
        }

        async public Task<bool> SaveAsync()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        async public Task<bool> CreateTagAsync(int postId, Tag tag)
        {
            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();

            var postTag = new PostTag()
            {
                Post = post,
                Tag = tag
            };

            _context.Add(postTag);

            _context.Add(tag);

            return await SaveAsync();
        }

        async public Task<bool> DeletePostAsync(Post post)
        { 
            _context.Remove(post);
            return await SaveAsync();
        }

        async public Task<PostTag?> GetPostTagAsync(int postId, int tagId)
        {
            return await _context.PostTags.Where(p => p.TagId == tagId && p.PostId == postId)
                .FirstOrDefaultAsync();
        }

        async public Task<bool> DeletePostTagAsync(PostTag postTag)
        {
            _context.Remove(postTag);
            return await SaveAsync();
        }

        async public Task<bool> CreatePostAsync(string authorEmail, Post post)
        {
            var userEntity = _context.Users.Where(p => p.Email == authorEmail).FirstOrDefault();

            post.Author = userEntity;

            _context.Add(post);

            return await SaveAsync();
        }
    }
}
