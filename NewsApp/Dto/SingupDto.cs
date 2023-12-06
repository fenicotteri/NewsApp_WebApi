using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class SingupDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
