﻿namespace NewsApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Post Post { get; set; } 
        public User Author { get; set; }
    }
}
