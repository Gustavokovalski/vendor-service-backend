using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VendorService.Domain.Services.Entities;

namespace VendorService.Application.Mappers
{
    public class SalesOrderModel
    {
        public int? Id { get; set; }
        public string CostumerEmail { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }

    }
}
