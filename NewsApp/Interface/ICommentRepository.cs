using NewsApp.Models;

namespace NewsApp.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsAsync();
        Task<Comment?> GetCommentAsync(int id); 
        public Task<bool> CreateCommentAsync(string authorEmail, int postId, Comment comment);
        public Task<bool> SaveAsync();
        public Task<bool> DeletecommentAsync(Comment comment);

        public bool CommentExists(int id);
    }
}
