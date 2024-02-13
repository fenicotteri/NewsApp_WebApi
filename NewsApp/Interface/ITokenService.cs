using NewsApp.Models;

namespace NewsApp.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
