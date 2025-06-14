using DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ITSchool.DAL.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        public ICollection<CourseTeacher> CourseTeachers { get; set; }
    }
}
