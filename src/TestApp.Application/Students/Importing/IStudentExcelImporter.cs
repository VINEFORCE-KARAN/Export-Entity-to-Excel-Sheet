using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Students.Importing
{
    public interface IStudentExcelImporter
    {
        Task ImportFromExcelAsync(IFormFile file);
    }
}
