using IKEA.BLL.DTO_S.Departments;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using System.Data;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {//Services => Departments

        private IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
        }
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departmentServices.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();
            return View(department);
        }
        #endregion

        #region Create
        [HttpGet]

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]

        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return View(departmentDto);

            var Message = string.Empty;
            try
            {
                var Result = departmentServices.CreateDepartment(departmentDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
                //1.log Exception Kestral
                logger.LogError(ex, ex.Message);

                //2.Set Default Message To User
                if (environment.IsDevelopment())
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);
                }
                else
                {
                    Message = "An Error Effect in The Creation Operator";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);
                }

            }
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)

        {
            if (id is null)
                return BadRequest();

            var Department = departmentServices.GetDepartmentById(id.Value);
            if (Department is null)
                return NotFound();

            var MappedDepartment = new UpdatedDepartmentDto()
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                CreationDate = Department.CreationDate,
            };

            return View(MappedDepartment);
        }

        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return View(departmentDto);
            var Message = String.Empty;
            try
            {
                var Result = departmentServices.UpdateDepartment(departmentDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department Is Not Updated";
            }

            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error Has Been Occured!";
            }

            ModelState.AddModelError(string.Empty, Message);
            return View(departmentDto);
        }
            #endregion

        #region Delete
            [HttpGet]
            public IActionResult Delete (int? id) 
            {
             if (id is null) 
                    return BadRequest();

                var Department = departmentServices.GetDepartmentById(id.Value);

                if(Department is null)
                    return NotFound();

                return View(Department);
            }
        [HttpPost]
        public IActionResult Delete(int Deptid)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = departmentServices.DeleteDepartment(Deptid);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                Message = "Department Is Not Deleted";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error Has Been Occured during deleted the Department!";
            }
            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new {id=Deptid});

        }

            #endregion








        }
    }


