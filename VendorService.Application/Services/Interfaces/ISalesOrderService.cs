using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Models;

namespace VendorService.Application.Services.Interfaces
{
    public interface ISalesOrderService
    {
        Task<BaseModel<SalesOrderModel>> Create(SalesOrderModel salesOrderModel);
        Task<BaseModel<SalesOrderModel>> Update(SalesOrderModel salesOrderModel);
        Task<BaseModel<SalesOrderModel>> Delete(int id);
        Task<BaseModel<SalesOrderModel>> GetById(int id);
        Task<BaseModel<List<ProductOrderModel>>> GetByOrderId(int id);
        Task<BaseModel<List<SalesOrderModel>>> List();
        string SendEmail(string message);
    }
}
