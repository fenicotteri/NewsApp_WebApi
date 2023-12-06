using NewsApp.Models;

namespace NewsApp.Interface
{
    public interface IRatingRepository
    {
        int GetValuePostRating(int id);
        int GetValueUserRating(int id);
    }
}
