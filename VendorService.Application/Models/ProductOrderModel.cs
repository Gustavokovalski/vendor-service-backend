using AutoMapper;

namespace VendorService.Application.Mappers
{
    public class ProductOrderModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
