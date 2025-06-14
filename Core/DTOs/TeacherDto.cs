using Core.DTOs;
using Microsoft.AspNetCore.Http;

namespace ITSchool.Core.DTOs
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<CourseSimpleDto> Courses { get; set; }
    }
    
    public class CreateTeacherDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public List<int>? CourseIds { get; set; }
    }
    
    public class UpdateTeacherDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public List<int>? CourseIds { get; set; }
    }
}
