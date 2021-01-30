using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using VendorService.Domain.Entities;
using VendorService.Domain.Enums;
using VendorService.Domain.Repositories;
using VendorService.Domain.Services.Entities;

namespace VendorService.Infra.Data.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly string connectionString;

        public SalesOrderRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<SalesOrder> Create(SalesOrder order)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                var query = " INSERT INTO SalesOrder (Email) OUTPUT INSERTED.Id VALUES (@email); ";
                connection.Open();
                var transaction = connection.BeginTransaction();
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@email", order.CostumerEmail);
                //cmd.Parameters.AddWithValue("@price", order.Price);
                cmd.Transaction = transaction;

                var id = await cmd.ExecuteScalarAsync();


                //await cmd.ExecuteNonQueryAsync();

                //order.ProductOrders.ForEach(item =>
                //    InserirProdutoPedido(item, pedido, connection, transaction));

                transaction.Commit();
                connection.Close();
                return order;
            }
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

