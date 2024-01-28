using AutoMapper;
using Habitaru.Models;
using Habitaru.ViewModels;
using Microsoft.AspNetCore.Routing.Internal;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Habitaru.AutoMapperConfig
{
    public class AutomapperConfig
    {
        public static Mapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<UserRegisterationVM, ApplicationUser>()
                    .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Username));

                cfg.CreateMap<UserRegisterationVM, ApplicationUser>()
                    .ForMember(dest => dest.PasswordHash, act => act.MapFrom(src => src.Password));

                cfg.CreateMap<UserLoginVM, ApplicationUser>()
                    .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Username));


                cfg.CreateMap<UserLoginVM, ApplicationUser>()
                    .ForMember(dest => dest.PasswordHash, act => act.MapFrom(src => src.Password));

            });
         
            return new Mapper(config);
        }
    }
}
