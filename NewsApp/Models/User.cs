using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace NewsApp.Models
{
    public class User : IdentityUser
    {
        // public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        // public string Email { get; set; } = string.Empty;

        public byte[]? FileBin { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedAt { get; set; } = DateTime.Now.ToUniversalTime();

        [JsonIgnore]
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; } 
    }
}
