using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string? CoverPath { get; set; }
        public int CommentsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("AuthorId")]
        public User Author { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
    }
}
