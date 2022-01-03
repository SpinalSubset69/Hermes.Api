using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.entities
{
    public class Article : BaseEntity
    {
        public string Title {get; set;}
        public string Summary {get; set;}
        public string Content {get; set;}
        public DateTime Created_At {get; set;}
        public int Likes {get; set;}        
        public List<Image> Images {get; set;}
        [ForeignKey("Reporter")]
        public int ReporterId {get; set;}
        [ForeignKey("Category")]
        public int CategoryId {get; set;}
        public Reporter Reporter {get; set;}
        public Category Category {get; set;}
        
        
    }
}