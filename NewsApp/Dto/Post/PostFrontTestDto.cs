namespace NewsApp.Dto.Post
{
    public class PostFrontTestDto
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Заголовок поста</example>
        public string Title { get; set; } = string.Empty;

        /// <example>Содержание поста</example>
        public string Text { get; set; } = string.Empty;
    }
}
