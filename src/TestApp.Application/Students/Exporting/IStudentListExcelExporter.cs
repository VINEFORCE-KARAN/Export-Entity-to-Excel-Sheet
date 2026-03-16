using System.Collections.Generic;
using TestApp.Dto;
using TestApp.Roles.Dto;
using TestApp.Students.Dto;

namespace TestApp.Students.Exporting
{
    public interface IStudentListExcelExporter
    {
        byte[] ExportToFile(List<StudentDto> students);
    }
}