using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Domain.Services.Entities;

namespace VendorService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Create(Product productModel);
        Task<Product> Update(Product productModel);
        Task<Product> Delete(int id);
        Task<Product> GetById(int id);
        Task<List<Product>> List();
    }
}
