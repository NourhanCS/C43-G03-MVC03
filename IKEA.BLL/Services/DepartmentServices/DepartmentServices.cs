using IKEA.BLL.DTO_S.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Departments;
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
        private IDepartmentRepository Repository;

        public DepartmentServices(IDepartmentRepository _repository)
        {
            Repository = _repository;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = Repository.GetAll().Select(dept => new DepartmentDto()
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

            var department = Repository.GetById(id);
            if (department is not null)
                return new DepartmentDetailsDto()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
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
            return Repository.Add(CreatedDepartment);
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
            return Repository.Update(UpdatedDepartment);
        }
        public bool DeleteDepartment(int id)
        {
            var department=Repository.GetById(id);
            //int result = 0;
            if (department is not null)
            return Repository.Delete(department)>0;
            //if(result>0)
            //    return true;
            else
                return false;
        }

    }
}
