using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Example.Model;
using Example.Repository.Common;
using Npgsql;


namespace Example.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public static string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=password;SSL Mode=Prefer;Timeout=10";
        public async Task<bool> PostEmployeeAsync(Employee employee)
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

                    int numberOfRowsAffected = await command.ExecuteNonQueryAsync();

                    if (numberOfRowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {   
            List<Employee> employees = new List<Employee>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = "SELECT * FROM employee";

                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read()&&reader.HasRows)
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
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = "SELECT * FROM employee WHERE Id=@Id";

                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            string email = reader.GetString(3);
                            string phoneNumber = reader.GetString(4);

                            Employee employee = new Employee(id, firstName, lastName, email, phoneNumber);

                            return employee;


                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public async Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee)
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

                    int numberOfRowsAffected = await command.ExecuteNonQueryAsync();

                    if (numberOfRowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = "DELETE FROM employee WHERE Id = @Id";

                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("Id", id);

                    int numberOfRowsAffected = await command.ExecuteNonQueryAsync();

                    if (numberOfRowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}

