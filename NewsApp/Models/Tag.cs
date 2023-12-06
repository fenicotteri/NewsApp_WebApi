namespace NewsApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<PostTag> PostTags { get; set; }

    }
}
