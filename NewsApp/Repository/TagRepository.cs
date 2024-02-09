using NewsApp.Data;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Tag?> GetTags(int postId)
        {
            return _context.PostTags.Where(p => p.PostId == postId).Select(t => t.Tag).ToList();
        }

    }
}
