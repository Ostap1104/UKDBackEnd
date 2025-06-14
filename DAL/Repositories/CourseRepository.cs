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
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public CourseRepository(ApplicationDbContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses
                .Include(c => c.CourseTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.CourseTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return null;

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);

            if (courseDto.Image != null)
            {
                course.ImageUrl = await _photoService.UploadImageAsync(courseDto.Image, "courses");
            }

            if (courseDto.TeacherIds != null && courseDto.TeacherIds.Any())
            {
                course.CourseTeachers = courseDto.TeacherIds
                    .Select(teacherId => new CourseTeacher
                    {
                        TeacherId = teacherId
                    }).ToList();
            }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> UpdateCourseAsync(int id, UpdateCourseDto courseDto)
        {
            var course = await _context.Courses
                .Include(c => c.CourseTeachers)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return null;

            _mapper.Map(courseDto, course);

            if (courseDto.Image != null)
            {
                if (!string.IsNullOrEmpty(course.ImageUrl))
                    await _photoService.DeleteImageAsync(course.ImageUrl);

                course.ImageUrl = await _photoService.UploadImageAsync(courseDto.Image, "courses");
            }

            if (courseDto.TeacherIds != null)
            {
                course.CourseTeachers.Clear();

                course.CourseTeachers = courseDto.TeacherIds
                    .Select(teacherId => new CourseTeacher
                    {
                        CourseId = id,
                        TeacherId = teacherId
                    }).ToList();
            }

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseDto>(course);
        }


        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.CourseTeachers)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return false;

            if (!string.IsNullOrEmpty(course.ImageUrl))
                await _photoService.DeleteImageAsync(course.ImageUrl);

            _context.CourseTeachers.RemoveRange(course.CourseTeachers);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
