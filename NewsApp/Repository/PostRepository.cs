using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NewsApp.Data;
using NewsApp.Helper;
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

        public async Task<List<Post>> GetPostsAsync(QueryObject query)
        {
            var response = _context.Posts.Include(a => a.Author).AsQueryable();
            //IQueryable<Post> response = _context.Posts.Include(a => a.Author);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                response = response.Where(x => x.Title.Contains(query.Search) || x.Text.Contains(query.Search));
            }

            if (!string.IsNullOrWhiteSpace(query.Author))
            {
                response = response.Where(x => x.Author.Email.Contains(query.Author) || x.Author.FirstName.Contains(query.Author) || x.Author.LastName.Contains(query.Author));
            }
            
            response = query.IsDecsending ? response.OrderByDescending(s => s.UpdatedAt) : response;
             

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            response = response.Skip(skipNumber).Take(query.PageSize);

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
