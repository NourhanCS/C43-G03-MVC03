using AutoMapper;
using IKEA.BLL.DTO_S.Departments;
using IKEA.PL.ViewModel;

namespace IKEA.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentVM, CreatedDepartmentDto>().ReverseMap();
            // .ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name));
            CreateMap<DepartmentDetailsDto, DepartmentVM>().ReverseMap();
            CreateMap<DepartmentVM, UpdatedDepartmentDto>().ReverseMap();



        }

    }
}

