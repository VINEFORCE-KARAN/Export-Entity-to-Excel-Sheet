using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestApp.Students;
using TestApp.Students.Dto;

namespace TestApp.Students.Importing
{
    public class StudentExcelImporter : IStudentExcelImporter, ITransientDependency
    {
        private readonly IRepository<Student, int> _studentRepository;

        public StudentExcelImporter(IRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentDto>> ImportFromExcelAsync(IFormFile file)
        {
            ExcelPackage.License.SetNonCommercialPersonal("TestApp");

            var students = new List<StudentDto>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        int age = 0;
                        int.TryParse(worksheet.Cells[row, 2].Text, out age); // 🔥 safe parse

                        var student = new StudentDto
                        {
                            Name = worksheet.Cells[row, 1].Text?.Trim(),
                            Age = age,
                            Course = worksheet.Cells[row, 3].Text?.Trim(),
                            Email = worksheet.Cells[row, 4].Text?.Trim()
                        };

                        students.Add(student);
                    }
                }
            }

            return students;
        }
    }
}