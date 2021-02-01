using System;
using System.Collections.Generic;
using System.Text;

namespace VendorService.Domain.Services.Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
