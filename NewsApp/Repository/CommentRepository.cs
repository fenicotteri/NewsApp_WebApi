using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            return await _context.Comments.OrderBy(x => x.Id)
                .Include(a => a.Author)
                .Include(p => p.Post)
               .ToListAsync();
        }

        public bool CommentExists(int id)
        {
            return _context.Comments.Where(c => c.Id == id).Any();
        }

        async public Task<bool> SaveAsync()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        async public Task<bool> CreateCommentAsync(string authorEmail, int postId, Comment comment)
        {
            var userEntity = _context.Users.Where(p => p.Email == authorEmail).FirstOrDefault();
            var postEntity = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();

            comment.Author = userEntity;
            comment.Post = postEntity;


            comment.CreatedAt = DateTime.Now;
            comment.UpdatedAt = DateTime.Now;

            _context.Add(comment);

            return await SaveAsync();
        }

        async public Task<bool> DeletecommentAsync(Comment comment)
        {
            _context.Remove(comment);
            return await SaveAsync();
        }

        async public Task<Comment> GetCommentAsync(int id)
        {
            return await _context.Comments.Where(c => c.Id == id)
                .Include(a => a.Author)
                .Include(p => p.Post)
                .FirstOrDefaultAsync();
        }
    }
}
