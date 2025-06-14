using AutoMapper;
using Core.DTOs;
using ITSchool.Core.DTOs;
using ITSchool.DAL.Models;

namespace DAL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Full <-> Simple
            CreateMap<Teacher, TeacherSimpleDto>();
            CreateMap<Course, CourseSimpleDto>();

            // Teacher mappings
            CreateMap<Teacher, TeacherDto>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src =>
                    src.CourseTeachers.Select(ct => ct.Course)));

            CreateMap<CreateTeacherDto, Teacher>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CourseTeachers, opt => opt.Ignore());

            CreateMap<UpdateTeacherDto, Teacher>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CourseTeachers, opt => opt.Ignore());


            // Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src =>
                    src.CourseTeachers.Select(ct => ct.Teacher))); 

            CreateMap<CreateCourseDto, Course>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                 .ForMember(dest => dest.CourseTeachers, opt => opt.Ignore()); 

            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CourseTeachers, opt => opt.Ignore());

            // User mappings
            CreateMap<User, UserDto>();
        }
    }
}
