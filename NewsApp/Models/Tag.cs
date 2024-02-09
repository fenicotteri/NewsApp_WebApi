﻿namespace NewsApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<PostTag>? PostTags { get; set; }

    }
}
