using System.ComponentModel.DataAnnotations;

namespace Hermes.API.Dtos
{
    public class RegisterCategory
    {
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Name { get; set; }    
    }
}
