using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class LoginDto
    {
        /// <example>mag2003tag@test.ru</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <example> 12345 </example>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
