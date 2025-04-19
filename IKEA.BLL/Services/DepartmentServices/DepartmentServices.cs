using IKEA.BLL.DTO_S.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {    //Controller => Service => Repository => Context => Options 

        //Repository
       // private IDepartmentRepository Repository;
        private readonly IUnitOfWork unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = unitOfWork.DepartmentRepository.GetAll().Where(D => !D.IsDeleted).Select(dept => new DepartmentDto()
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate,
            }).ToList();
            return Departments;
        }

        //   List<DepartmentDto>departmentDtos = new List<DepartmentDto>();
        //    foreach(var dept in Departments)
        //    {
        //        DepartmentDto departmentDto = new DepartmentDto()
        //        {
        //            Id = dept.Id,
        //            Name = dept.Name,
        //            Code = dept.Code,
        //            CreationDate = dept.CreationDate,
        //        };
        //        departmentDtos.Add(departmentDto);
        //    }

       
        public DepartmentDetailsDto? GetDepartmentById(int id)

        {

            var department = unitOfWork.DepartmentRepository.GetById(id);
            if (department is not null)
                return new DepartmentDetailsDto()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    IsDeleted = department.IsDeleted,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,

                };

            return null;
            
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                CreatedOn=DateTime.Now,
                LastModifiedBy =1,
                LastModifiedOn=DateTime.Now,
            };
            unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return unitOfWork.Complete();
        }
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var UpdatedDepartment = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy=1,
                LastModifiedOn=DateTime.Now,
            };
           unitOfWork.DepartmentRepository.Update(UpdatedDepartment);
            return unitOfWork.Complete();
        }
        public bool DeleteDepartment(int id)
        {
            var department=unitOfWork.DepartmentRepository.GetById(id);
            //int result = 0;
            if (department is not null)
             unitOfWork.DepartmentRepository.Delete(department);
            var Result = unitOfWork.Complete();
            if (Result < 0)
                return true;
            //if(result>0)
            //    return true;
            else
                return false;
        }

    }
}
