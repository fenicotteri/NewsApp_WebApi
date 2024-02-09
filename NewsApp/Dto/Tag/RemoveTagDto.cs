using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class RemoveTagDto
    {
        /// <example>1</example>
        [Required]
        public int TagId {  get; set; }
    }
}
