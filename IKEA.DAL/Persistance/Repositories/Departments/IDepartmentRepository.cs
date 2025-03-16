using IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{//GetALL GetById Add Update Delete 
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool WithNoTracking=true);
      
        Department? GetById(int id);

        int Add (Department department);

        int Update (Department department);

        int Delete (Department department);
    }
}
