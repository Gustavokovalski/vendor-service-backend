using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Domain.Entities;
using VendorService.Domain.Services.Entities;

namespace VendorService.Domain.Repositories
{
    public interface ISalesOrderRepository
    {
        Task<SalesOrder> Create(SalesOrder order);
        Task<SalesOrder> Update(SalesOrder order);
        Task<SalesOrder> Delete(int id);
        Task<SalesOrder> GetById(int id);
        Task<List<SalesOrder>> List();
    }
}