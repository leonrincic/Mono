using ExampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace ExampleWebApi.Controllers
{
    public class StudentController : ApiController
    {
        private static List<Student> students = new List<Student>();
        static int lastId = 0;

        static int GenerateId()
        {
            return Interlocked.Increment(ref lastId);
        }

        [HttpPost]
        public HttpResponseMessage PostStudent(Student student)
        {
            student.Id = GenerateId();
            students.Add(student);
            return Request.CreateResponse(HttpStatusCode.OK,"Student succesfully posted to base");
        }

        [HttpGet]
        public HttpResponseMessage GetStudents()
        {
            var studentViewModels = students.Select(s => new StudentViewModel(s)).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, studentViewModels);
        }

        [HttpGet]
        public HttpResponseMessage GetStudentById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            var viewStudentModel = new StudentViewModel(student);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, viewStudentModel);
        }

        [HttpPut]
        public HttpResponseMessage UpdateStudent(int id, Student student)
        {
            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid student data");
            }

            var existingStudent = students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Year = student.Year;

            StudentViewModel studentViewModel = new StudentViewModel(existingStudent);

            return Request.CreateResponse(HttpStatusCode.OK, studentViewModel);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            students.Remove(student);
            return Request.CreateResponse(HttpStatusCode.OK, "Student removed succesfully");
        }
    }
}
