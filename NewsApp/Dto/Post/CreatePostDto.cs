using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class CreatePostDto
    {
        /// <example>Заголовок поста</example>
        [Required]
        [MinLength(1, ErrorMessage = "Title must be over one character")]
        [MaxLength(1000, ErrorMessage = "Title can not be over 1000 characters")]
        public string? Title { get; set; } 

        /// <example>Содержание поста</example>
        [Required]
        public string? Text { get; set; } 
    }
}
