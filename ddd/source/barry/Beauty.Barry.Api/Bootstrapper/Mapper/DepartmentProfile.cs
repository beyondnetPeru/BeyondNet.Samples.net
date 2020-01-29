using AutoMapper;
using Beauty.Barry.Application.Dto.Department;
using Beauty.Barry.Domain.Department;

namespace Beauty.Barry.Api.Bootstrapper.Mapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentEdit, DepartmentDto>();

            CreateMap<DepartmentEdit, DepartmentUpdateDto>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForSourceMember(src => src.Audit, opt => opt.DoNotValidate())
             .ForSourceMember(src => src.Id, opt => opt.DoNotValidate())
             .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<DepartmentUpdateDto, DepartmentEdit>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.Status, opt => opt.Ignore())
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.IsNew, opt => opt.Ignore())
             .ForMember(dest => dest.IsDirty, opt => opt.Ignore())
             .ForMember(dest => dest.IsValid, opt => opt.Ignore())
             .ForMember(dest => dest.BrokenRules, opt => opt.Ignore());           
        }
    }
}
