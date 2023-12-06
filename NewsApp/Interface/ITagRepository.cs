using NewsApp.Models;

namespace NewsApp.Interface
{
    public interface ITagRepository
    {
        public ICollection<Tag> GetTags(int postId);
    }
}
