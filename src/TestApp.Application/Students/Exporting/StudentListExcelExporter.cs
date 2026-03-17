using System.Collections.Generic;
using System.Linq;
using TestApp.Dto;
using TestApp.Students.Dto;
using OfficeOpenXml;
using System.IO;
using Abp.Dependency;

namespace TestApp.Students.Exporting
{
    public class StudentListExcelExporter : IStudentListExcelExporter , ITransientDependency
    {
        public byte[] ExportToFile(List<StudentDto> students)
        {
            ExcelPackage.License.SetNonCommercialPersonal("TestApp");

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Students");

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Age";
                worksheet.Cells[1, 3].Value = "Course";

                int row = 2;

                foreach (var student in students)
                {
                    worksheet.Cells[row, 1].Value = student.Name;
                    worksheet.Cells[row, 2].Value = student.Age;
                    worksheet.Cells[row, 3].Value = student.Course;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}