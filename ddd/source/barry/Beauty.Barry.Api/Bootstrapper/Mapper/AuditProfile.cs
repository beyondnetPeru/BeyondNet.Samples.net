using AutoMapper;
using Beauty.Dick.Domain.Model.Audit;
using Beauty.Dick.Helpers.Domain.Impl;

namespace Beauty.Barry.Api.Bootstrapper.Mapper
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditDto, Audit>()
             .ForMember(dest => dest.CreateUser, opt => opt.MapFrom(src => src.CreateUser))
             .ForMember(dest => dest.CreateDevice, opt => opt.MapFrom(src => src.CreateDevice))
             .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
             .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.UpdateUser))
             .ForMember(dest => dest.UpdateDevice, opt => opt.MapFrom(src => src.UpdateDevice))
             .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
             .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.TimeSpan))
             .ForMember(dest => dest.BrokenRules, opt => opt.Ignore())
             .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
        }
    }
}
