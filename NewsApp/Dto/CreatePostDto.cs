using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class CreatePostDto
    {
        /// <example>Заголовок поста</example>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <example>Содержание поста</example>
        [Required]
        public string Text { get; set; } = string.Empty;
    }
}
