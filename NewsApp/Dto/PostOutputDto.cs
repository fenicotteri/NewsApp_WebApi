using NewsApp.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class PostOutputDto
    {
        /// <example> 1 </example>
        [Required]
        public int Id { get; set; }

        /// <example>Заголовок поста</example>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <example>Содержание поста</example>
        [Required]
        public string Text { get; set; } = string.Empty;

        /// <example>/images/1685004747837-194473147.png</example>
        [Required]
        public string? CoverPath { get; set; }

        /// <example> 1 </example>
        [Required]
        public int AuthorId { get; set; }

        [Required]
        public PublicUserDto? Author { get; set; }
        // public ICollection<TagOutputDto>? Tags { get; set; }

        /// <example> 1 </example>
        [Required]
        public int CommentsCount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

    }
}
