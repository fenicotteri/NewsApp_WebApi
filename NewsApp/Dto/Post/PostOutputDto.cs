using NewsApp.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class PostOutputDto
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Заголовок поста</example>
        public string Title { get; set; } = string.Empty;

        /// <example>Содержание поста</example>
        public string Text { get; set; } = string.Empty;

        /// <example>/images/1685004747837-194473147.png</example>
        public string? CoverPath { get; set; }

        /// <example> 1 </example>
        public string? AuthorId { get; set; }

        public PublicUserDto? Author { get; set; }
        // public ICollection<TagOutputDto>? Tags { get; set; }

        /// <example> 1 </example>
        public int CommentsCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
