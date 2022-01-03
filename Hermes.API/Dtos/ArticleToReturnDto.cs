using Core.entities;

namespace Hermes.API.Dtos
{
    public class ArticleToReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime Created_At { get; set; }
        public int Likes { get; set; }
        public string Category { get; set; }
        public List<ImageToReturnDto> Images { get; set; }
        public ReporterToReturnDto Reporter { get; set; }   
    }
}
