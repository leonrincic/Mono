using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model;

namespace Example.Service.Common
{
    public interface IEmployeeService
    {
        Task<bool> PostEmployeeAsync(Employee employee);
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
