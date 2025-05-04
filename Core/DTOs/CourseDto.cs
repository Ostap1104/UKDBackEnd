using Microsoft.AspNetCore.Http;

namespace ITSchool.Core.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public string ImageUrl { get; set; }
    }
    
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public IFormFile Image { get; set; }
    }
    
    public class UpdateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public IFormFile? Image { get; set; }
    }
}
