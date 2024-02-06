using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class PostOutputWithAuthorDto
    {
        [Required]
        public List<PostOutputDto> Posts {  get; set; }

        /// <example> 30 </example>
        [Required]
        public int Total {  get; set; }
    }
}
