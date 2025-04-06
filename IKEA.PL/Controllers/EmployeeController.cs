using IKEA.BLL.DTO_S.Departments;
using IKEA.BLL.DTO_S.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services - DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }

        #endregion

        #region Index 

        [HttpGet]   
        public ActionResult Index()
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
            return View();

        }

        [HttpPost]

        public IActionResult Create(CreatedEmployeeDto EmployeeDto)
        {
            if (!ModelState.IsValid)
                return View(EmployeeDto);

            var Message = string.Empty;
            try
            {
                var Result = employeeServices.CreateEmployee(EmployeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department is not Created";
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
            return View(EmployeeDto);
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

            var MappedEmployee = new UpdatedEmployeeDto()
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

            return View(MappedEmployee);
        }

        [HttpPost]
        public IActionResult Edit(UpdatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);
            var Message = String.Empty;
            try
            {
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

            ModelState.AddModelError(string.Empty, Message);
            return View(employeeDto);
        }
        #endregion



    }
}
