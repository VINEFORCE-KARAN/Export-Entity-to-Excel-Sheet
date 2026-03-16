using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Dto;
using TestApp.Students.Dto;
using TestApp.Students.Exporting;
using Microsoft.AspNetCore.Mvc;


namespace TestApp.Students
{
    public class StudentAppService : ApplicationService
    {
        private readonly IRepository<Student, int> _studentRepository;
        private readonly IStudentListExcelExporter _studentListExcelExporter;

        public StudentAppService(IRepository<Student, int> studentRepository,
               IStudentListExcelExporter studentListExcelExporter)

        {
            _studentRepository = studentRepository;
            _studentListExcelExporter = studentListExcelExporter;

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

        // Export students to Excel

        public async Task<FileContentResult> GetStudentsToExcel()
        {
            var students = await _studentRepository.GetAllListAsync();
            var studentDtos = ObjectMapper.Map<List<StudentDto>>(students);

            var fileBytes = _studentListExcelExporter.ExportToFile(studentDtos);

            return new FileContentResult(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            )
            {
                FileDownloadName = "Students.xlsx"
            };
        }


    }
}