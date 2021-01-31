using System;
using System.Collections.Generic;
using System.Text;

namespace VendorService.Domain.Services.Entities
{
    public class SalesOrder
    {
        public int? Id { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        public decimal OrderTotalPrice { get; set; }
    }
}
