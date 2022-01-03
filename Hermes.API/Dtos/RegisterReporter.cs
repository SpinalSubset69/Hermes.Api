using System.ComponentModel.DataAnnotations;

namespace Hermes.API.Dtos
{
    public class RegisterReporter
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RolId { get; set; }
    }
}
