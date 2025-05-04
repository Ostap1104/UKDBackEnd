using System;
using System.ComponentModel.DataAnnotations;

namespace ITSchool.DAL.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string ShortDescription { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [MaxLength(100)]
        public string Duration { get; set; }
        
        public string ImageUrl { get; set; }
    }
}
