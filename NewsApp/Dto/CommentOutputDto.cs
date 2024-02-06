using NewsApp.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class CommentOutputDto
    {
        /// <example> 1 </example>
        [Required]
        public int Id { get; set; }

        /// <example>Текст комментария</example>
        [Required]
        public string Text { get; set; } = string.Empty;

        /// <example> 1 </example>
        [Required]
        public int AuthorId { get; set; }

        /// <example> 1 </example>
        [Required]
        public int PostId { get; set; }

        [Required]
        public PublicUserDto Author { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        [Required]
        public DateTime? UpdatedAt { get; set; }

    }
}
