using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Domain.Services.Entities;

namespace VendorService.Domain.Repositories
{
    public interface IProductOrderRepository
    {
        Task<List<ProductOrder>> GetByOrderId(int id);
    }
}
