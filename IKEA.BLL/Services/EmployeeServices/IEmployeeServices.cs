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
        IEnumerable<EmployeeDto> GetAllEmployees();
       EmployeeDetailsDto? GetEmployeeById(int id);

        int CreateEmployee(CreatedEmployeeDto employeeDto);

        int UpdateEmployee(UpdatedEmployeeDto employeeDto);

        bool DeleteEmployee(int id);

    }
}
