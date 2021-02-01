using AutoMapper;
using VendorService.Domain.Services.Entities;

namespace VendorService.Application.Mappers
{
    public class SalesOrderModelMapper : Profile
    {
        public SalesOrderModelMapper()
        {
            CreateMap<SalesOrder, SalesOrderModel>()
                 .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                 .ForMember(p => p.CustomerEmail, p => p.MapFrom(x => x.CustomerEmail))
                 .ForMember(p => p.PurchaseDate, p => p.MapFrom(x => x.PurchaseDate))
                 .ForMember(p=>p.OrderTotalPrice, p=>p.MapFrom(x=>x.OrderTotalPrice));

            CreateMap<SalesOrderModel, SalesOrder>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.CustomerEmail, p => p.MapFrom(x => x.CustomerEmail))
                .ForMember(p => p.PurchaseDate, p => p.MapFrom(x => x.PurchaseDate))
                .ForMember(p => p.OrderTotalPrice, p => p.MapFrom(x => x.OrderTotalPrice));
        }
    }
}
