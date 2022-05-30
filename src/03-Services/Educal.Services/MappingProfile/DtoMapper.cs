using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Educal.Core.Dtos;
using Educal.Core.Models;

namespace Educal.Services.MappingProfile
{
    public class DTOMapper : Profile
    {
        public DTOMapper()
        {
            CreateMap<Customer,CustomerDto>().ReverseMap();
            CreateMap<Instructor,InstructorDto>().ReverseMap();
            CreateMap<Manager,ManagerDto>().ReverseMap();
            CreateMap<Registrar,RegistrarDto>().ReverseMap();
            CreateMap<Student,StudentDto>().ReverseMap();
            CreateMap<Lesson,LessonDto>().ReverseMap();
            CreateMap<WorkingTime,WorkingTimeDto>().ReverseMap();
            CreateMap<Classroom,ClassroomDto>().ReverseMap();
        }
    }
}