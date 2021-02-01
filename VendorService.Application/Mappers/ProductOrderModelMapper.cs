using AutoMapper;
using VendorService.Domain.Services.Entities;

namespace VendorService.Application.Mappers
{
    public class ProductOrderModelMapper : Profile
    {
        public ProductOrderModelMapper()
        {
            CreateMap<ProductOrder, ProductOrderModel>()
                 .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                 .ForMember(p => p.OrderId, p => p.MapFrom(x => x.OrderId))
                 .ForMember(p => p.ProductId, p => p.MapFrom(x => x.ProductId))
                 .ForMember(p=>p.Quantity, p=>p.MapFrom(x=>x.Quantity))
                 .ForMember(p=>p.ProductPrice, p=>p.MapFrom(x=>x.ProductPrice))
                 .ForMember(p => p.TotalPrice, p => p.MapFrom(x => x.TotalPrice));

            CreateMap<ProductOrderModel, ProductOrder>()
                 .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                 .ForMember(p => p.OrderId, p => p.MapFrom(x => x.OrderId))
                 .ForMember(p => p.ProductId, p => p.MapFrom(x => x.ProductId))
                 .ForMember(p => p.Quantity, p => p.MapFrom(x => x.Quantity))
                 .ForMember(p => p.ProductPrice, p => p.MapFrom(x => x.ProductPrice))
                 .ForMember(p => p.TotalPrice, p => p.MapFrom(x => x.TotalPrice));
        }
    }
}
