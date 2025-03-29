using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {//Repository => Context => Options
        private readonly ApplicationDbContext dbContext;

        public DepartmentRepository(ApplicationDbContext context) //Ask CLR for Generating Object Of Context
        {
           dbContext = context;

        }

        public IEnumerable<Department> GetAll(bool WithNoTracking=true)
        {
            if (WithNoTracking)
                return dbContext.Departments.Where(D=>D.IsDeleted==false).AsNoTracking().ToList();

            return dbContext.Departments.Where(D => D.IsDeleted == false).ToList();
        }

        public Department? GetById(int id)
        {
            var Department = dbContext.Departments.Find(id);

            //var Department= dbContext.Departments.Local.SingleOrDefault(D=>D.Id==id);

            //if (Department is null)
            //    Department = dbContext.Departments.FirstOrDefault(D => D.Id == id);
                
            return Department;
        }
        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }
        public int Update(Department department)
        {
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            department.IsDeleted= true;
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }

       
    }
}
