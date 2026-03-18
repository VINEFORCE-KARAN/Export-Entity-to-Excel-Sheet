using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Students.Dto;

namespace TestApp.Students.Importing
{
    public interface IStudentExcelImporter
    {
        Task<List<StudentDto>> ImportFromExcelAsync(IFormFile file);
    }
}
