using NewsApp.Models;

namespace NewsApp.Dto
{
    public class PostOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string? CoverPath { get; set; }
        public int AuthorId { get; set; }
        public PublicUserDto? Author { get; set; }
        // public ICollection<TagOutputDto>? Tags { get; set; }
        public int CommentsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
