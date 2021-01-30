using System;
using System.Collections.Generic;
using System.Text;

namespace VendorService.Domain.Services.Entities
{
    public class ProductOrder
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
