using NewsApp.Models;

namespace NewsApp.Interface
{
    public interface IPostRepository
    {
        Task<Post> GetPostAsync(int id);
        Task<PostTag> GetPostTagAsync(int postId, int tagId);
        Task<List<Post>> GetPostsAsync();
        bool PostExists(int id);
        public Task<bool> CreateTagAsync(int postId, Tag tag);
        public Task<bool> SaveAsync();
        public Task<bool> DeletePostAsync(Post post);
        public Task<bool> DeletePostTagAsync(PostTag postTag);
    }
}
