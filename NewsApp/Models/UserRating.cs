namespace NewsApp.Models
{
    public class UserRating
    {
        public int Id { get; set; }
        public byte Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CountRate { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }

    }
}
