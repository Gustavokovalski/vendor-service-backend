using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Domain.Services.Entities;

namespace VendorService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Create(Product product);
        Task<Product> GetById(int id);
        Task<List<Product>> List();
        Task<Product> Update(Product product);
        //Task<Product> Delete(int id);
    }
}
