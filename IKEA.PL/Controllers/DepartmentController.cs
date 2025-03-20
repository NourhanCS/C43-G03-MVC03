using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {//Services => Departments

        private IDepartmentServices departmentServices;

        public DepartmentController(IDepartmentServices _departmentServices)
        {
            departmentServices = _departmentServices;
        }
        #region Index
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            return View(Departments);
        } 
        #endregion
    }
}
