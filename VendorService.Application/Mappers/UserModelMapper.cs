using AutoMapper;
using VendorService.Application.Models;
using VendorService.Domain.Entities;
using VendorService.Domain.Enums;
using VendorService.Shared;

namespace VendorService.Application.Mappers
{
    public class UserModelMapper : Profile
    {
        public UserModelMapper()
        {
            CreateMap<User, UserModel>()
               .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
               .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
               .ForMember(p => p.ProfileId, p => p.MapFrom(x => x.Profile.GetEnumValue()))
               .ForMember(p => p.Profile, p => p.MapFrom(x => new EnumModel(x.Profile)))
               .ForMember(p => p.Password, p => p.MapFrom(x => x.Password))
               .ForMember(p => p.Token, p => p.Ignore());

            CreateMap<UserModel, User>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Profile, p => p.MapFrom(x => x.ProfileId.Value.GetEnum<EProfiles>()))
                .ForMember(p => p.Password, p => p.MapFrom(x => x.Password));
        }
    }
}
