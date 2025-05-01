using AutoMapper;
using ITSchool.Core.DTOs;
using ITSchool.DAL.Models;

namespace DAL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Teacher mappings
            CreateMap<Teacher, TeacherDto>();
            CreateMap<CreateTeacherDto, Teacher>();
            CreateMap<UpdateTeacherDto, Teacher>();
            
            // Course mappings
            CreateMap<Course, CourseDto>();
            CreateMap<CreateCourseDto, Course>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); 
            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); ;

            // User mappings
            CreateMap<User, UserDto>();
        }
    }
}
