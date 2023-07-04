using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleWebApi.Models
{
    public class StudentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Year { get; set; }
        public StudentViewModel(Student student)
        {
            FirstName = student.FirstName;
            LastName = student.LastName;
            Year = student.Year;
        }
    }
}