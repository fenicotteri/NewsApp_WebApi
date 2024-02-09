using NewsApp.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class CommentOutputDto
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Текст комментария</example>
        public string Text { get; set; } = string.Empty;

        /// <example>1</example>
        public int AuthorId { get; set; }

        /// <example>1</example>
        public int PostId { get; set; }

        public PublicUserDto? Author { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
