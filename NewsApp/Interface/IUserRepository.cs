using Microsoft.AspNetCore.JsonPatch;
using NewsApp.Models;

namespace NewsApp.Interface
{
    public interface IUserRepository
    {
        public Task<ICollection<User>> GetUsersAsync();

        public Task<User?> GetUserAsync(int id);

        public Task<User?> GetUserAsync(string email);

        public Task<bool> CreateUserAsync(User user);

        bool UserExists(int id);
        public Task<bool> SaveAsync();
        public Task<bool> UpdateUserPatchAsync(User user, JsonPatchDocument<User> userRequest);

    }
}
