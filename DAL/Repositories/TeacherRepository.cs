using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Services;
using DAL.Models;
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
        private readonly IPhotoService _photoService;

        public TeacherRepository(ApplicationDbContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await _context.Teachers
               .Include(t => t.CourseTeachers)
                   .ThenInclude(ct => ct.Course)
               .ToListAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.CourseTeachers)
                    .ThenInclude(ct => ct.Course)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return null;
            }

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto teacherDto)
        {
            var teacher = _mapper.Map<Teacher>(teacherDto);

            if(teacherDto.Image != null)
            {
                teacher.ImageUrl = await _photoService.UploadImageAsync(teacherDto.Image, "teachers");
            }

            if (teacherDto.CourseIds != null && teacherDto.CourseIds.Any())
            {
                teacher.CourseTeachers = teacherDto.CourseIds
                    .Select(courseId => new CourseTeacher
                    {
                        CourseId = courseId
                    }).ToList();
            }
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto)
        {
            var teacher = await _context.Teachers
                .Include(t => t.CourseTeachers)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return null;
            }

            _mapper.Map(teacherDto, teacher);

            if (teacherDto.Image != null)
            {
                
                if (!string.IsNullOrEmpty(teacher.ImageUrl))
                {
                    await _photoService.DeleteImageAsync(teacher.ImageUrl);
                }


                var imageUrl = await _photoService.UploadImageAsync(teacherDto.Image, "teachers");
                teacher.ImageUrl = imageUrl;
            }
            if (teacherDto.CourseIds != null)
            {
                teacher.CourseTeachers.Clear();

                teacher.CourseTeachers = teacherDto.CourseIds
                    .Select(courseId => new CourseTeacher
                    {
                        TeacherId = id,
                        CourseId = courseId
                    }).ToList();
            }
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.CourseTeachers)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return false;
            } 

            if (!string.IsNullOrEmpty(teacher.ImageUrl))
            {
                await _photoService.DeleteImageAsync(teacher.ImageUrl);
            }

            _context.CourseTeachers.RemoveRange(teacher.CourseTeachers);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
