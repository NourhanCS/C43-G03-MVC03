using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    internal interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; set; }

        public IEmployeeRepository EmployeeRepository { get; set; }

        int Complete();


    }
}
