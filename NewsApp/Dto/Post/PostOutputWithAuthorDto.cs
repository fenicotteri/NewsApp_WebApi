using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class PostOutputWithAuthorDto
    {
        public List<PostOutputDto>? Posts {  get; set; }

        /// <example> 30 </example>
        public int Total {  get; set; }
    }
}
