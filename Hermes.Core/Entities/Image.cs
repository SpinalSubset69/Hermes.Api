using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.entities
{
    public class Image : BaseEntity
    {
        public string Name {get; set;}

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}