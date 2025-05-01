using System.Collections.Generic;
using System.Threading.Tasks;
using ITSchool.Core.DTOs;

namespace ITSchool.Core.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto);
        Task<CourseDto> UpdateCourseAsync(int id, UpdateCourseDto courseDto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
