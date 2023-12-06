namespace NewsApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;

        public byte[]? PasswordSalt { get; set; }
        public byte[]? AvatarPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte[]? PasswordHash { get; set; }

        public UserRating Rating { get; set; }
        public ICollection<Post> posts { get; set; }
        public ICollection<Comment> comments { get; set; }
    }
}
