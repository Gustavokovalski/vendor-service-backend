using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

using VendorService.Domain.Repositories;
using VendorService.Domain.Services.Entities;

namespace VendorService.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product> Create(Product productModel)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> List()
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(Product productModel)
        {
            throw new NotImplementedException();
        }
    }
}
