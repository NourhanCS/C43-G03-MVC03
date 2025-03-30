using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{//GetALL GetById Add Update Delete 
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
       
    }
}
