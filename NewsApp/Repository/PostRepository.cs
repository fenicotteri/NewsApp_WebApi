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
                .Include(r => r.Rating)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _context.Posts.OrderBy(x => x.Id)
                .Include(p => p.Rating)
                .Include(a => a.Author)
                .Include(t => t.PostTags)
                    .ThenInclude(postTag => postTag.Tag)
                .ToListAsync();
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

            tag.CreatedAt = DateTime.Now;
            tag.UpdatedAt = DateTime.Now;

            _context.Add(tag);

            return await SaveAsync();
        }

        async public Task<bool> DeletePostAsync(Post post)
        { 
            _context.Remove(post);
            return await SaveAsync();
        }

        async public Task<PostTag> GetPostTagAsync(int postId, int tagId)
        {
            return await _context.PostTags.Where(p => p.TagId == tagId && p.PostId == postId)
                .FirstOrDefaultAsync();
        }

        async public Task<bool> DeletePostTagAsync(PostTag postTag)
        {
            _context.Remove(postTag);
            return await SaveAsync();
        }
    }
}
