using IKEA.BLL.DTO_S.Departments;
using IKEA.BLL.DTO_S.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public interface IEmployeeServices
    {
      Task<IEnumerable<EmployeeDto>> GetAllEmployees(string search);
      Task<EmployeeDetailsDto>? GetEmployeeById(int id);

      Task<int> CreateEmployee(CreatedEmployeeDto employeeDto);

      Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto);

      Task<bool> DeleteEmployee(int id);

    }
}
