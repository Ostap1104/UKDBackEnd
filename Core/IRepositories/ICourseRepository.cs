using System.Collections.Generic;
using System.Threading.Tasks;
using ITSchool.Core.DTOs;

namespace ITSchool.Core.IRepositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto);
        Task<CourseDto> UpdateCourseAsync(int id, UpdateCourseDto courseDto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
