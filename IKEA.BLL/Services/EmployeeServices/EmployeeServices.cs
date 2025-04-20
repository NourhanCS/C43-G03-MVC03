using IKEA.BLL.Common.Services.Attachments;
using IKEA.BLL.DTO_S.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Repositories.Employees;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IKEA.BLL.Services.EmployeeServices
{
   public class EmployeeServices:IEmployeeServices
    { 
     // private readonly IEmployeeRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAttachmentServices attachmentServices;

        public EmployeeServices(IUnitOfWork unitOfWork ,IAttachmentServices attachmentServices)
        {
            this.unitOfWork = unitOfWork;
            this.attachmentServices = attachmentServices;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees(string search)
        {
            var Employee = unitOfWork.EmployeeRepository.GetAll();

            var FilteredEmployees = Employee.Where(E => E.IsDeleted == false && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())));
                var AfterFilteration=FilteredEmployees.Include(E=>E.Department).Select(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = E.Gender,
                EmployeeType = E.EmployeeType,
                Department = E.Department.Name ?? "N/A" 
            });
            return await AfterFilteration.ToListAsync();

        }

        public async Task<EmployeeDetailsDto>? GetEmployeeById(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetById(id);

            if (employee is not null)
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber= employee.PhoneNumber,
                    HiringDate= employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    Department = employee.Department?.Name ?? "N/A",
                    ImageName = employee.ImageName,

                };
            }

            return null;
        }

        public async Task<int> CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = new Employeee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy= 1,
                LastModifiedOn=DateTime.Now,
                
            };

            if (employeeDto.Image is not null)
            {
          Employee.ImageName = attachmentServices.UploadImage(employeeDto.Image,"images");
            }

        unitOfWork.EmployeeRepository.Add(Employee);
            return await unitOfWork.Complete();
        }

        public async Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var Employee = new Employeee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                ImageName = employeeDto.ImageName

            };

            if (employeeDto.Image is not null)
            {
                if (Employee.ImageName is not null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", Employee.ImageName);
                    attachmentServices.DeleteImage(filePath);
                }
                Employee.ImageName = attachmentServices.UploadImage(employeeDto.Image, "images");
            }

            unitOfWork.EmployeeRepository.Update(Employee);
            return await unitOfWork.Complete();
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetById(id);
            
            if (employee is not null)
            {
                if(employee.ImageName is not null)
                {
                 var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","files","images",employee.ImageName);
                    attachmentServices.DeleteImage(filePath);
                }
                unitOfWork.EmployeeRepository.Delete(employee);

            }
            var result = await unitOfWork.Complete();
            if (result>0)
                return true;
            else
                return false;
        } 

        
    }
}
