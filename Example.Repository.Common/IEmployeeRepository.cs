using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Example.Repository.Common
{
    public interface IEmployeeRepository
    {
        Task<bool> PostEmployeeAsync(Employee employee);
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee);
        Task<bool> DeleteEmployeeAsync(Guid id);

    }
}
