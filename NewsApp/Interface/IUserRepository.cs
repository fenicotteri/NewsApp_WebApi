using Microsoft.AspNetCore.JsonPatch;
using NewsApp.Models;

namespace NewsApp.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(int id);

        User GetUser(string email);

        public bool CreateUser(User user);

        public bool Save();

        bool UserExists(int id);
        public Task<bool> SaveAsync();
        public Task<bool> UpdateUserPatchAsync(User user, JsonPatchDocument<User> userRequest);

    }
}
