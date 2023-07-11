using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model;
using Example.Service.Common;
using Example.Repository;

namespace Example.Service
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<bool> PostEmployeeAsync(Employee employee)
        {
            return await new EmployeeRepository().PostEmployeeAsync(employee);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await new EmployeeRepository().GetEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await new EmployeeRepository().GetEmployeeByIdAsync(id);
        }

        public async Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee)
        {
            return await new EmployeeRepository().UpdateEmployeeAsync(id, updatedEmployee);
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            return await new EmployeeRepository().DeleteEmployeeAsync(id);
        }


    }
}
