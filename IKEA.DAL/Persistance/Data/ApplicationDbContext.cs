using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        //Dependancy Injection

        //Department => Context => Options
        //Ask CLR Generate Options for My Context
        public ApplicationDbContext(DbContextOptions options):base(options) 
        {
         
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Employeee> Employees { get; set; }

    }
}
