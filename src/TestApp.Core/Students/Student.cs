using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Students
{
    public class Student : Entity<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
