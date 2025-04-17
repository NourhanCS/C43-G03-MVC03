using IKEA.BLL.DTO_S.Departments;
using IKEA.BLL.DTO_S.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services - DI
        private readonly IEmployeeServices employeeServices;
       // private readonly IDepartmentServices departmentServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices,IDepartmentServices departmentServices ,ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
           // this.departmentServices = departmentServices;
            this.logger = logger;
            this.environment = environment;
        }

        #endregion

        #region Index 

        [HttpGet]   
        public IActionResult Index()
        {

            var Employees = employeeServices.GetAllEmployees();
            return View(Employees);
        }

        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = employeeServices.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();
            return View(employee);
        }
        #endregion

        #region Create
        [HttpGet]

        public IActionResult Create()
        {
          //  ViewData["Departments"] = departmentServices.GetAllDepartments();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(EmployeeVM employeeVM)
        {
            // ServerSide Validation
            if (!ModelState.IsValid) // false => BadRequest
                return View(employeeVM);

            var Message = string.Empty;
            try
            {
                var employeeDto = new CreatedEmployeeDto()
                {
                 Name = employeeVM.Name,
                 Age = employeeVM.Age,
                 Salary= employeeVM.Salary,
                 Address = employeeVM.Address,
                 PhoneNumber = employeeVM.PhoneNumber,
                 HiringDate = employeeVM.HiringDate,
                Email = employeeVM.Email,
                IsActive = employeeVM.IsActive,
                Gender = employeeVM.Gender,
                EmployeeType = employeeVM.EmployeeType,

                };
                var Result = employeeServices.CreateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Employee is not Created";
            }
            catch (Exception ex)
            {
                //1.log Exception Kestral
                logger.LogError(ex, ex.Message);

                //2.Set Default Message To User
                if (environment.IsDevelopment())
                    Message = ex.Message;
                else
                    Message = "An Error Effect in The Creation Operator";

            }
            ModelState.AddModelError(string.Empty, Message);
            return View(employeeVM);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)

        {
            if (id is null)
                return BadRequest();

            var Employee = employeeServices.GetEmployeeById(id.Value);
            if (Employee is null)
                return NotFound();

            var MappedEmployee = new EmployeeVM()
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
               Address = Employee.Address,
              HiringDate  = Employee.HiringDate,
              Email = Employee.Email,
              PhoneNumber = Employee.PhoneNumber,
              Salary = Employee.Salary, 
              Gender = Employee.Gender,
              EmployeeType = Employee.EmployeeType,
              IsActive = Employee.IsActive,
            };

           // ViewData["Departments"] = departmentServices.GetAllDepartments();

            return View(MappedEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);
            var Message = String.Empty;
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = employeeVM.Id,
                    Name = employeeVM.Name,
                    Age = employeeVM.Age,
                    Salary = employeeVM.Salary,
                    Address = employeeVM.Address,
                    PhoneNumber = employeeVM.PhoneNumber,
                    HiringDate = employeeVM.HiringDate,
                    Email = employeeVM.Email,
                    IsActive = employeeVM.IsActive,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,

                };

                var Result = employeeServices.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Employee Is Not Updated";
            }

            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error Has Been Occured during Update the Employee !";
            }
          //  ViewData["Departments"] = departmentServices.GetAllDepartments();
            ModelState.AddModelError(string.Empty, Message);
            return View(employeeVM);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var Employee = employeeServices.GetEmployeeById(id.Value);

            if (Employee is null)
                return NotFound();

            return View(Employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Empid)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = employeeServices.DeleteEmployee(Empid);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                Message = "Employee Is Not Deleted";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error Has Been Occured during deleted The Employee !";
            }
            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new { id = Empid });

        }

        #endregion




    }
}
