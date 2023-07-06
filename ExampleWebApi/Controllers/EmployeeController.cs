using System;
using ExampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Npgsql;
using System.Data.SqlClient;

namespace ExampleWebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public static string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=password;SSL Mode=Prefer;Timeout=10";


        [HttpPost]
        public HttpResponseMessage PostEmployee(Employee employee)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var query = "INSERT INTO employee (Id, FirstName, LastName, Email, PhoneNumber) VALUES (@Id , @FirstName, @LastName, @Email, @PhoneNumber)";

                    connection.Open();

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("Id", Guid.NewGuid());
                        command.Parameters.AddWithValue("FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("LastName", employee.LastName);
                        command.Parameters.AddWithValue("Email", employee.Email);
                        command.Parameters.AddWithValue("PhoneNumber", employee.PhoneNumber);

                        int numberOfRowsAffected = command.ExecuteNonQuery();

                        if (numberOfRowsAffected > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "Employee created successfully.");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "There was an error while trying to create employee");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while creating the employee.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    var query = "SELECT * FROM employee";

                    connection.Open();

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Guid id = reader.GetGuid(0);
                                    string firstName = reader.GetString(1);
                                    string lastName = reader.GetString(2);
                                    string email = reader.GetString(3);
                                    string phoneNumber = reader.GetString(4);

                                    Employee employee = new Employee(id, firstName, lastName, email, phoneNumber);
                                    employees.Add(employee);
                                }
                            }
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while retrieving the employees.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEmployeeById(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    var query = "SELECT * FROM employee WHERE Id=@Id";
                   
                    connection.Open();
                    
                    using(var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("Id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                string email = reader.GetString(3);
                                string phoneNumber = reader.GetString(4);

                                Employee employee = new Employee(id, firstName,lastName,email, phoneNumber);
                                return Request.CreateResponse(HttpStatusCode.OK, employee);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found.");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occured while retrieving the employee.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateEmployee(Guid id, Employee updatedEmployee)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    var query = "UPDATE employee SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber WHERE Id = @Id";
                    
                    connection.Open();
                   
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("Id", id);
                        command.Parameters.AddWithValue("FirstName", updatedEmployee.FirstName);
                        command.Parameters.AddWithValue("LastName", updatedEmployee.LastName);
                        command.Parameters.AddWithValue("Email", updatedEmployee.Email);
                        command.Parameters.AddWithValue("PhoneNumber", updatedEmployee.PhoneNumber);

                        int numberOfRowsAffected = command.ExecuteNonQuery();

                        if (numberOfRowsAffected > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "Employee updated successfully.");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while updating the employee.");
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    var query = "DELETE FROM employee WHERE Id = @Id";

                    connection.Open();

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("Id", id);

                        int numberOfRowsAffected = command.ExecuteNonQuery();

                        if (numberOfRowsAffected > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "Employee deleted successfully.");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while deleting the employee.");
            }
        }

    }
}
