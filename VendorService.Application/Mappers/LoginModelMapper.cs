using AutoMapper;
using VendorService.Domain.Entities;

namespace VendorService.Application.Mappers
{
    public class LoginModelMapper : Profile
    {
        public LoginModelMapper()
        {
            CreateMap<User, LoginModel>()
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Password, p => p.MapFrom(x => x.Password));

            CreateMap<LoginModel, User>()
                .ForMember(p => p.Id, p => p.Ignore())
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email));
        }
    }
}
