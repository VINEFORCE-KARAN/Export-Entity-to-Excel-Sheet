using Abp.Dependency;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TestApp.Dto;
using TestApp.Students.Dto;

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


                // 🔥 Header 

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Age";
                worksheet.Cells[1, 3].Value = "Course";
                worksheet.Cells[1, 4].Value = "Email";

                using (var headerRange = worksheet.Cells[1, 1, 1, 4])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Font.Size = 12;
                    headerRange.Style.Font.Color.SetColor(Color.White);

                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);

                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }


                int row = 2;

                foreach (var student in students)
                {
                    worksheet.Cells[row, 1].Value = student.Name;
                    worksheet.Cells[row, 2].Value = student.Age;
                    worksheet.Cells[row, 3].Value = student.Course;
                    worksheet.Cells[row, 4].Value = student.Email;
                    row++;
                }

                // 🔥 Table Borders
                var dataRange = worksheet.Cells[1, 1, row - 1, 4];

                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // 🔥 Auto Fit Columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // 🔥 Alternate Row Color (striped effect)
                for (int i = 2; i <= row - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        var range = worksheet.Cells[i, 1, i, 4];
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                }


                return package.GetAsByteArray();
            }
        }
    }
}