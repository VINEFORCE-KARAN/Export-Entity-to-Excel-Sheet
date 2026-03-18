using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Dto;
using TestApp.Students.Dto;
using TestApp.Students.Exporting;
using TestApp.Students.Importing;


namespace TestApp.Students
{
    public class StudentAppService : ApplicationService
    {
        private readonly IRepository<Student, int> _studentRepository;
        private readonly IStudentListExcelExporter _studentListExcelExporter;
        private readonly IStudentExcelImporter _studentListExcelImporter;

        public StudentAppService(IRepository<Student, int> studentRepository,
                                 IStudentListExcelExporter studentListExcelExporter,
                                 IStudentExcelImporter studentListExcelImporter)

        {
            _studentRepository = studentRepository;
            _studentListExcelExporter = studentListExcelExporter;
            _studentListExcelImporter = studentListExcelImporter;

        }

        public async Task CreateStudent(CreateStudentDto input)
        {

            var exists = await _studentRepository.FirstOrDefaultAsync(x =>
             x.Email == input.Email
                 );

            if (exists != null)
            {
                throw new UserFriendlyException("Email already exists!");
            }
            var student = ObjectMapper.Map<Student>(input);
            await _studentRepository.InsertAsync(student);

        }

        public async Task<List<StudentDto>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllListAsync();
            return ObjectMapper.Map<List<StudentDto>>(students);
        }

        // Duplicate Check 

        public async Task DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }

        public async Task UpdateStudent(UpdateStudentDto input)
        {
            var student = await _studentRepository.GetAsync(input.Id);

            // 🔥 Duplicate Email Check (excluding current record)
            var exists = await _studentRepository.FirstOrDefaultAsync(x =>
                x.Email == input.Email && x.Id != input.Id
            );

            if (exists != null)
            {
                throw new UserFriendlyException("Email already exists!");
            }
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

        // Import students from Excel 
        public async Task<string> ImportStudentsFromExcel(IFormFile file)
        {
            var students = await _studentListExcelImporter.ImportFromExcelAsync(file);

            var existingStudents = await _studentRepository.GetAllListAsync();

            // 🔥 FAST lookup
            var existingEmails = existingStudents
                .Where(x => !string.IsNullOrWhiteSpace(x.Email))
                .Select(x => x.Email.Trim().ToLower())
                .ToHashSet();

            var duplicateEmails = new List<string>();
            int successCount = 0;

            foreach (var studentDto in students)
            {
                var email = studentDto.Email?.Trim().ToLower();

                // 🔥 skip invalid email
                if (string.IsNullOrWhiteSpace(email))
                {
                    continue;
                }

                // 🔥 duplicate check (DB + Excel both)
                if (existingEmails.Contains(email))
                {
                    duplicateEmails.Add(studentDto.Email);
                    continue;
                }

                var student = ObjectMapper.Map<Student>(studentDto);
                await _studentRepository.InsertAsync(student);

                existingEmails.Add(email); // 🔥 important
                successCount++;
            }

            return $"Imported: {successCount}, Skipped duplicates: {duplicateEmails.Count}";
        }
    }
} 