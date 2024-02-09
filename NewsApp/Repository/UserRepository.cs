using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        { 
            _context = context;
        }

        public Task<bool> CreateUserAsync(User user)
        {
            _context.Add(user);
            return SaveAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _context.Users.OrderBy(x => x.Id).ToListAsync();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        async public Task<bool> SaveAsync()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        async public Task<bool> UpdateUserPatchAsync(User user, JsonPatchDocument<User> userRequest)
        {
            userRequest.ApplyTo(user);
            return await SaveAsync();

        }
    }
}
