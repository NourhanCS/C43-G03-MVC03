using IKEA.BLL.Common.Services.Attachments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.DAL.Models.Identity;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using IKEA.DAL.Persistance.UnitOfWork;
using IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IKEA.PL
{
    public class Program
    {
        //Entry Point
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services 
            builder.Services.AddControllersWithViews();//Object => Department => Srvices =>Repository => Context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
           //Allow Debendancy Injection => UserManager => SignInManager => RoleManager
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
                {

                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;  //#$%
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredUniqueChars = 1;


                    options.User.RequireUniqueEmail = true;
                   
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication().AddCookie(options =>
            {
                options.LoginPath = "/Account/LogIn";
                options.AccessDeniedPath = "/Home/Error";
                options.ExpireTimeSpan = TimeSpan.FromDays(2);
                options.ForwardSignOut = "/Account/LogIn";
            });
            
            //  builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();    

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();

            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();

            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();

            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfile)));          
            #endregion


            var app = builder.Build();

            #region Configure Pipelines (MiddleWares)
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see /*https://aka.ms/aspnetcore-hsts.*/
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            

          

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion
             

            app.Run();
            


        }
    }
}
 