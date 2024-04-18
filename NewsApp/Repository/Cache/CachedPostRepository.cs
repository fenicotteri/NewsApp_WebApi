using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using NewsApp.Helper;
using NewsApp.Interface;
using NewsApp.Models;
using Newtonsoft.Json;

namespace NewsApp.Repository.Cache
{
    public class CachedPostRepository : IPostRepository
    {
        private readonly IPostRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        // private readonly IMemoryCache _memoryCache;

        public CachedPostRepository(IPostRepository postRepository, IDistributedCache distributedCache)
        {
            _decorated = postRepository;
            _distributedCache = distributedCache;
            // _memoryCache = memoryCache;
        }

        public Task<bool> CreatePostAsync(string authorEmail, Post post) => _decorated.CreatePostAsync(authorEmail, post);

        public Task<bool> CreateTagAsync(int postId, Tag tag) => _decorated.CreateTagAsync(postId, tag);

        public Task<bool> DeletePostAsync(Post post) => _decorated.DeletePostAsync(post);

        public Task<bool> DeletePostTagAsync(PostTag postTag) => _decorated.DeletePostTagAsync(postTag);

        public async Task<Post?> GetPostAsync(int id)
        {
            string key = $"post-{id}";

            string? cachedPost = await _distributedCache.GetStringAsync(key);

            Post? post;

            if(string.IsNullOrEmpty(cachedPost))
            {
                post = await _decorated.GetPostAsync(id);   

                if(post is null)
                {
                    return post;
                }

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(post, settings));

                return post;
            }

            post = JsonConvert.DeserializeObject<Post>(cachedPost);

            return post;
            /*
            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return _decorated.GetPostAsync(id);
                }
                );
            */
        }

        public Task<List<Post>> GetPostsAsync(QueryObject query) => _decorated.GetPostsAsync(query);

        public Task<List<Post>> GetPostsWithTagsAsync() => _decorated.GetPostsWithTagsAsync();

        public Task<PostTag?> GetPostTagAsync(int postId, int tagId) => _decorated.GetPostTagAsync(postId, tagId);

        public bool PostExists(int id) => _decorated.PostExists(id);

        public Task<bool> SaveAsync()=> _decorated.SaveAsync();
    }
}
