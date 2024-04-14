using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Caching.Memory;
using NewsApp.Helper;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Repository.Cache
{
    public class CachedPostRepository : IPostRepository
    {
        private readonly IPostRepository _decorated;
        private readonly IMemoryCache _memoryCache;

        public CachedPostRepository(IPostRepository postRepository, IMemoryCache memoryCache)
        {
            _decorated = postRepository;
            _memoryCache = memoryCache;
        }

        public Task<bool> CreatePostAsync(string authorEmail, Post post)
        {
            return _decorated.CreatePostAsync(authorEmail, post);
        }

        public Task<bool> CreateTagAsync(int postId, Tag tag)
        {
            return _decorated.CreateTagAsync(postId, tag);
        }

        public Task<bool> DeletePostAsync(Post post)
        {
            return _decorated.DeletePostAsync(post);
        }

        public Task<bool> DeletePostTagAsync(PostTag postTag)
        {
            return _decorated.DeletePostTagAsync(postTag);
        }

        public Task<Post?> GetPostAsync(int id)
        {
            string key = $"post-{id}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return _decorated.GetPostAsync(id);
                }
                );
        }

        public Task<List<Post>> GetPostsAsync(QueryObject query)
        {
            return _decorated.GetPostsAsync(query);
        }

        public Task<List<Post>> GetPostsWithTagsAsync()
        {
            return _decorated.GetPostsWithTagsAsync();
        }

        public Task<PostTag?> GetPostTagAsync(int postId, int tagId)
        {
            return _decorated.GetPostTagAsync(postId, tagId);
        }

        public bool PostExists(int id)
        {
            return _decorated.PostExists(id);
        }

        public Task<bool> SaveAsync()
        {
            return _decorated.SaveAsync();
        }
    }
}
