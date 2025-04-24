﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTO_S.Departments
{
    public class UpdatedDepartmentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The Name Is Reqired")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The Code Is Reqired")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name="Date Of Creation")]
        public DateOnly CreationDate { get; set; }



    }
}
