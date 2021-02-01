using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VendorService.Domain.Services.Entities;

namespace VendorService.Application.Mappers
{
    public class SalesOrderModel
    {
        public int? Id { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<ProductOrderModel> ProductOrders { get; set; }
        public decimal OrderTotalPrice { get; set; }

    }
}
