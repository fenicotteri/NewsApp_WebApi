using NewsApp.Models;

namespace NewsApp.Dto
{
    public class CommentOutputDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public PublicUserDto Author { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
