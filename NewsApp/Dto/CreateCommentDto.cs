using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class CreateCommentDto
    {
        /// <example>Текст комментария</example>
        [Required]
        public string Text { get; set; }

        /// <example> 1 </example>
        [Required]
        public int PostId { get; set; }
    }
}
