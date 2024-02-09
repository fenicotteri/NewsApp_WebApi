using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class UpdateUserDto
    {
        /// <example>Мария</example>
        [Required]
        public string FirstName { get; set; } = string.Empty;

        /// <example>Гаврилова</example>
        [Required]
        public string LastName { get; set; } = string.Empty;

        public IFormFile? FileBin { get; set; }

        /// <example>mag2003tag@test.com</example>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <example> 12345 </example>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
