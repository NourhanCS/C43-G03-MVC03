using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext context) //Ask CLR for Generating Object Of Context
        {
            dbContext = context;

        }

        public IQueryable<T> GetAll(bool WithNoTracking = true)
        {// In-Memory Collection
            if (WithNoTracking)
                return dbContext.Set<T>().AsNoTracking();

            return dbContext.Set<T>();
        }

        public T? GetById(int id)
        {
            var item = dbContext.Set<T>().Find(id);

            //var Employee= dbContext.Employees.Local.SingleOrDefault(E => E.Id==id);

            //if (Employee is null)
            //    Employee = dbContext.Emloyees.FirstOrDefault(E => E.Id == id);

            return item;
        }
        public int Add(T item)
        {
            dbContext.Set<T>().Add(item);
            return dbContext.SaveChanges();
        }
        public int Update(T item)
        {
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            item.IsDeleted = true;
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();
        }

      
    }
}
