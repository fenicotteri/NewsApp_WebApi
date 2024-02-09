using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class TagOutputDto
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>#tag</example>
        public string Value { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
