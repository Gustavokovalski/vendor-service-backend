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
                 .ForMember(p => p.CostumerEmail, p => p.MapFrom(x => x.CostumerEmail))
                 .ForMember(p => p.PurchaseDate, p => p.MapFrom(x => x.PurchaseDate))
                 .ReverseMap();
        }
    }
}
