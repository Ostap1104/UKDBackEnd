using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITSchool.Core.DTOs;
using ITSchool.Core.IRepositories;
using ITSchool.DAL.Data;
using ITSchool.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ITSchool.DAL.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeacherRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return null;
            }

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto teacherDto)
        {
            var teacher = _mapper.Map<Teacher>(teacherDto);

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return null;
            }

            _mapper.Map(teacherDto, teacher);

            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return false;
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
