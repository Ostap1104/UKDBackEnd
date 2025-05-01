using System.Collections.Generic;
using System.Threading.Tasks;
using ITSchool.Core.DTOs;
using ITSchool.Core.IRepositories;

namespace ITSchool.Core.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAllTeachersAsync();
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            return await _teacherRepository.GetTeacherByIdAsync(id);
        }

        public async Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto teacherDto)
        {
            return await _teacherRepository.CreateTeacherAsync(teacherDto);
        }

        public async Task<TeacherDto> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto)
        {
            return await _teacherRepository.UpdateTeacherAsync(id, teacherDto);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            return await _teacherRepository.DeleteTeacherAsync(id);
        }
    }
}
