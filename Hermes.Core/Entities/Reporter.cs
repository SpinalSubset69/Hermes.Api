using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.entities
{
    public class Reporter : BaseEntity
    {
        public string Name {get; set;}        
        public string UserName {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}        
        public string? Image { get; set; }

        [ForeignKey("Role")]
        public int RolId {get; set;}
        public Role Role {get; set;}
        public IEnumerable<Article>? Articles {get; set;}        
    }
}