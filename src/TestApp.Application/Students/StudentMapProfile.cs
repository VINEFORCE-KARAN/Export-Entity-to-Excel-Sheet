using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Students.Dto;
using AutoMapper;

namespace TestApp.Students
{
    public class StudentMapProfile : Profile
    {
        public StudentMapProfile()
        {
          //CreateMap<Source, Destination>()
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
            CreateMap<Student, StudentDto>();
            
        }
    }
}
