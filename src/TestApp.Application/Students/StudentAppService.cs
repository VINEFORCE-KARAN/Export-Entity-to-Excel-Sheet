using Abp.Application.Services;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Students.Dto;

//namespace TestApp.Students
//{
//    public class StudentAppService : ApplicationService
//    {

//        //IRepository<TEntity, TPrimaryKey>
//        private readonly IRepository<Student, int> _studentRepository;

//        public StudentAppService(IRepository<Student, int> studentRepository)
//        {
//            _studentRepository = studentRepository;
//        }

//        public async Task CreateStudent(CreateStudentDto input)
//        {
//            var student = ObjectMapper.Map<Student>(input);

//            await _studentRepository.InsertAsync(student);
//        }

//        public async Task<List<StudentDto>> GetAllStudents()
//        {
//            var students = await _studentRepository.GetAllListAsync();

//            return ObjectMapper.Map<List<StudentDto>>(students);
//        }
//    }

//}

namespace TestApp.Students
{
    public class StudentAppService : ApplicationService
    {
        private readonly IRepository<Student, int> _studentRepository;

        public StudentAppService(IRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
            
        }

        public async Task CreateStudent(CreateStudentDto input)
        {
            var student = ObjectMapper.Map<Student>(input);
            await _studentRepository.InsertAsync(student);

        }

        public async Task<List<StudentDto>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllListAsync();
            return ObjectMapper.Map<List<StudentDto>>(students);
        }
        public async Task DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }

        public async Task UpdateStudent(UpdateStudentDto input)
        {
            var student = await _studentRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, student);
            await _studentRepository.UpdateAsync(student);
        }


    }
}