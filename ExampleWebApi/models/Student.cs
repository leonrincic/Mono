using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int Year { get; set; }
    public int Age { get; set; }

    public Student(int id, string firstname, string lastname,int year, int age)
    {
        Id = id;
        FirstName = firstname;
        LastName = lastname;
        Year = year;
        Age = age;
    }
}