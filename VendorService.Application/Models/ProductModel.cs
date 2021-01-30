using AutoMapper;

namespace VendorService.Application.Mappers
{
    public class ProductModel : Profile
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

    }
}
