using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class CountryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
