using AutoMapper;
using VendorService.Application.Models;
using VendorService.Domain.Entities;
using VendorService.Domain.Enums;
using VendorService.Domain.Extensions;

namespace VendorService.Application.Mappers
{
    public class UserRegisterModelMapper : Profile
    {
        public UserRegisterModelMapper()
        {
            CreateMap<User, UserRegisterModel>()
               .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
               .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
               .ForMember(p => p.ProfileId, p => p.MapFrom(x => x.Profile.GetEnumValue()))
               .ForMember(p => p.Profile, p => p.MapFrom(x => new EnumModel(x.Profile)))
               .ForMember(p => p.Password, p => p.MapFrom(x => x.Password))
               .ForMember(p => p.Token, p => p.Ignore());

            CreateMap<UserRegisterModel, User>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Profile, p => p.MapFrom(x => x.ProfileId.Value.GetEnum<EProfiles>()))
                .ForMember(p => p.Password, p => p.MapFrom(x => x.Password));

        }
    }
}
