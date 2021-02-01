using AutoMapper;
using VendorService.Domain.Services.Entities;

namespace VendorService.Application.Mappers
{
    public class ProductModelMapper : Profile
    {
        public ProductModelMapper()
        {
            CreateMap<Product, ProductModel>()
               .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
               .ForMember(p => p.Name, p => p.MapFrom(x => x.Name))
               .ForMember(p => p.Price, p => p.MapFrom(x => x.Price))
               .ForMember(p => p.Active, p => p.MapFrom(x => x.Active));

            CreateMap<ProductModel, Product>()
                 .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
               .ForMember(p => p.Name, p => p.MapFrom(x => x.Name))
               .ForMember(p => p.Price, p => p.MapFrom(x => x.Price))
               .ForMember(p => p.Active, p => p.MapFrom(x => x.Active));

        }
    }
}
