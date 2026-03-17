using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using Abp.Domain.Repositories;
using Abp.Dependency;
using TestApp.Students;

namespace TestApp.Students.Importing
{
    public class StudentExcelImporter : IStudentExcelImporter, ITransientDependency
    {
        private readonly IRepository<Student, int> _studentRepository;

        public StudentExcelImporter(IRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task ImportFromExcelAsync(IFormFile file)
        {
            ExcelPackage.License.SetNonCommercialPersonal("TestApp");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // row 1 = header skip
                    {
                        var name = worksheet.Cells[row, 1].Text;
                        var ageText = worksheet.Cells[row, 2].Text;
                        var course = worksheet.Cells[row, 3].Text;

                        // Skip empty rows
                        if (string.IsNullOrWhiteSpace(name))
                            continue;

                        int age = 0;
                        int.TryParse(ageText, out age);

                        var student = new Student
                        {
                            Name = name,
                            Age = age,
                            Course = course
                        };

                        await _studentRepository.InsertAsync(student);
                    }
                }
            }
        }
    }
}