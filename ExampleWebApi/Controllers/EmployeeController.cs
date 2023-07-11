using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Npgsql;
using System.Data.SqlClient;
using Example.Model;
using Example.Service;
using System.Threading.Tasks;

namespace ExampleWebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public static string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=password;SSL Mode=Prefer;Timeout=10";


        [HttpPost]
        public async Task<HttpResponseMessage> PostEmployee(Employee employee)
        {
            try
            {
                EmployeeService service = new EmployeeService();
                bool newEmployee = await service.PostEmployeeAsync(employee);
                if (newEmployee == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Employee created successfully.");

                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "There was an error while trying to create employee");
                    
                

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while creating the employee.");
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetEmployees()
        {
            
            try
            {
                EmployeeService service = new EmployeeService();
                List<Employee> employees = await service.GetEmployeesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while retrieving the employees.");
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetEmployeeById(Guid id)
        {
            try
            {  
                EmployeeService service = new EmployeeService();
                Employee employee = await service.GetEmployeeByIdAsync(id);
                if(employee != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occured while retrieving the employee.");
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateEmployee(Guid id, Employee updatedEmployee)
        {
            try
            {
                EmployeeService service = new EmployeeService();
                Employee existingEmployee = await service.GetEmployeeByIdAsync(id);
                if(existingEmployee == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Employee not found.");
                }
                if(updatedEmployee.FirstName == null)
                {
                    updatedEmployee.FirstName = existingEmployee.FirstName;
                }
                if (updatedEmployee.LastName == null)
                {
                    updatedEmployee.LastName = existingEmployee.LastName;
                }
                if (updatedEmployee.Email == null) { 
                    updatedEmployee.Email = existingEmployee.Email;
                }
                if(updatedEmployee.PhoneNumber == null)
                {
                    updatedEmployee.PhoneNumber = existingEmployee.PhoneNumber;
                }


                
                
                bool isUpdated = await service.UpdateEmployeeAsync(existingEmployee.Id, updatedEmployee);
                if(isUpdated)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Employee has been updated.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");



            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while updating the employee.");
            }
        }
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteEmployee(Guid id)
        {
            try
            {
                EmployeeService service = new EmployeeService();
                bool isDeleted = await service.DeleteEmployeeAsync(id);
                if(isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Employee has been deleted.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while deleting the employee.");
            }
        }

    }
}
