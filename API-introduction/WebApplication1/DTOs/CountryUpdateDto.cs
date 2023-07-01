using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class CountryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
