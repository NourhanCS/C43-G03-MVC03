using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories._Generic;
using IKEA.DAL.Persistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Employees
{
    public class EmployeeRepository :GenericRepository<Employeee> ,IEmployeeRepository
    {//Repository => Context => Options

        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext context):base(context) //Ask CLR for Generating Object Of Context
        {
            dbContext = context;

        }





      
    }
}

