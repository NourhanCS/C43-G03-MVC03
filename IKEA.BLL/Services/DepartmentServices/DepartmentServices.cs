using IKEA.BLL.DTO_S.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments()
        {
            var Departments = await unitOfWork.DepartmentRepository.GetAll().Where(D => !D.IsDeleted).Select(dept => new DepartmentDto()
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate,
            }).ToListAsync();
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

       
        public async Task<DepartmentDetailsDto>? GetDepartmentById(int id)

        {

            var department = await unitOfWork.DepartmentRepository.GetById(id);
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

        public async Task<int> CreateDepartment(CreatedDepartmentDto departmentDto)
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
            return await unitOfWork.Complete();
        }
        public async Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto)
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
            return await unitOfWork.Complete();
        }
        public async Task<bool> DeleteDepartment(int id)
        {
            var department= await unitOfWork.DepartmentRepository.GetById(id);
            //int result = 0;
            if (department is not null)
             unitOfWork.DepartmentRepository.Delete(department);
            var Result = await unitOfWork.Complete();
            if (Result < 0)
                return true;
            //if(result>0)
            //    return true;
            else
                return false;
        }

    }
}
