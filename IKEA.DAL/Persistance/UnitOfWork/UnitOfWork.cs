using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public IDepartmentRepository DepartmentRepository { get ; }
        public IEmployeeRepository EmployeeRepository { get ; }

        public UnitOfWork(ApplicationDbContext dbContext)   //Ask CLR To Generate Object from Context
        {
            this.dbContext = dbContext;

            DepartmentRepository = new DepartmentRepository(this.dbContext);

            EmployeeRepository = new EmployeeRepository(this.dbContext);

        }

        public async Task <int> Complete()
        { 
         return await dbContext.SaveChangesAsync();
        }
    }
}
