using AutoMapper;
using BeyondNet.App.Ums.Domain.User;
using BeyondNet.App.Ums.Domain.User.Dto;

namespace BeyondNet.App.Ums.Api.Bootstrapper.AutoMapperConfig
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<UserEdit, UserInfoReadModel>()
                    .ForSourceMember(src => src.Audit, opt => opt.Ignore())
                    .ForSourceMember(src => src.Keys, opt => opt.Ignore())
                    .ForSourceMember(src => src.Status, opt => opt.Ignore());

                config.CreateMap<UserInfoReadModel, UserInfoDto>();

                config.CreateMap<UserInfoDto, UserDto>();

                config.CreateMap<UserEdit, UserDto>()
                    .ForSourceMember(src => src.Audit, opt => opt.Ignore());
            });
        }
    }
}

