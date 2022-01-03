using System.ComponentModel.DataAnnotations;

namespace Hermes.API.Dtos
{
    public class RegisterArticle
    {
        [Required(ErrorMessage = "Title must be provided")]
        [MinLength(2)]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Summary must be provided")]
        [MinLength(2)]
        [MaxLength(150)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Content must be provided")]
        [MinLength(15)]        
        public string Content { get; set; }

        [Required(ErrorMessage ="Category Id must be provided")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Reporter Id must be provided")]
        public int ReporterId { get; set; }
    }
}
