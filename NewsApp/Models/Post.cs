using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string? CoverPath { get; set; }
        public int CommentsCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedAt { get; set; } = DateTime.Now.ToUniversalTime();

        [ForeignKey("AuthorId")]
        public User? Author { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
    }
}
