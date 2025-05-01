using System.Collections.Generic;
using System.Threading.Tasks;
using ITSchool.Core.DTOs;

namespace ITSchool.Core.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto> GetTeacherByIdAsync(int id);
        Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto teacherDto);
        Task<TeacherDto> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
