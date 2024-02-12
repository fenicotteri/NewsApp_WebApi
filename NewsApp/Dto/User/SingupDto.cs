using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class SingupDto
    {
        /// <example>mag2003tag@gmail.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <example> 12345 </example>
        [Required]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Password should be larger.")]
        public string Password { get; set; } = string.Empty;
    }
}
