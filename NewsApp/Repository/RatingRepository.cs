using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DataContext _context;
        public RatingRepository(DataContext context)
        {
            _context = context;
        }

        public int GetValuePostRating(int id)
        {
            var rating = _context.PostRatings.Where(r => r.Id == id).FirstOrDefault();
            return rating.Value;
        }

        public int GetValueUserRating(int id)
        {
            var rating = _context.UserRatings.Where(r => r.Id == id).FirstOrDefault();
            return rating.Value;
        }
    }
}
