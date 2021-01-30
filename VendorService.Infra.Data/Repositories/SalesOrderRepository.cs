using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using VendorService.Domain.Entities;
using VendorService.Domain.Enums;
using VendorService.Domain.Repositories;
using VendorService.Domain.Services.Entities;
using VendorService.Shared;

namespace VendorService.Infra.Data.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly string connectionString;

        public SalesOrderRepository()
        {
            connectionString = ConfigurationHelper.ConnectionString;
        }

        public Task<SalesOrder> Create(SalesOrder order)
        {
            throw new NotImplementedException();
        }

        public Task<SalesOrder> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SalesOrder> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SalesOrder>> List()
        {
            throw new NotImplementedException();
        }

        public Task<SalesOrder> Update(SalesOrder order)
        {
            throw new NotImplementedException();
        }
    }

    
}

