using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using VendorService.Domain.Repositories;
using VendorService.Domain.Services.Entities;


namespace VendorService.Infra.Data.Repositories
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private readonly string connectionString;

        public ProductOrderRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        

        public async Task<List<ProductOrder>> GetByOrderId(int id)
        {
            var productOrders = new List<ProductOrder>();
            SqlCommand sqlcmd;
            string query = " SELECT Id, ProductId, OrderId, Quantity, ProductPrice, TotalPrice FROM ProductOrder WHERE OrderId = @id; ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@id", id.ToString());
                using (var reader = await sqlcmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productOrders.Add(new ProductOrder()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal(nameof(ProductOrder.Id))),
                            ProductId = reader.GetInt32(reader.GetOrdinal(nameof(ProductOrder.ProductId))),
                            OrderId = reader.GetInt32(reader.GetOrdinal(nameof(ProductOrder.OrderId))),
                            ProductPrice = reader.GetDecimal(reader.GetOrdinal(nameof(ProductOrder.ProductPrice))),
                            Quantity = reader.GetInt32(reader.GetOrdinal(nameof(ProductOrder.Quantity))),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal(nameof(ProductOrder.TotalPrice)))
                        });
                    }
                }
            }
            return productOrders;
        }

    }
}
