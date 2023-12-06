namespace NewsApp.Models
{
    public class PostRating
    {
        public int Id { get; set; }
        public byte Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CountRate { get; set; }
        public Post Post { get; set; }
        public int? PostId { get; set; }
    }
}
