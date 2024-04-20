using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class LoginDto
    {
        /// <example>mag2003tag@gmail.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <example>>MagTag456?</example>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
