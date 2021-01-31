using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            SqlCommand sqlcmd;
            SqlTransaction transaction;
            string query = " INSERT INTO SalesOrder (Email, PurchaseDate) OUTPUT INSERTED.Id VALUES (@email, @purchaseDate); ";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                try
                {
                    sqlcmd.Connection = connection;
                    sqlcmd.Transaction = transaction;
                    sqlcmd.CommandText = query;
                    sqlcmd.Parameters.AddWithValue("@email", order.CustomerEmail);
                    sqlcmd.Parameters.AddWithValue("@purchaseDate", order.PurchaseDate);

                    var id = await sqlcmd.ExecuteScalarAsync();

                    foreach (var product in order.ProductOrders)
                    {
                        SqlCommand sqlcmdProduct = connection.CreateCommand();
                        string queryProducts = @" INSERT INTO ProductOrder (ProductId, OrderId, Quantity, ProductPrice, TotalPrice) 
                                                  VALUES (@productId, @orderId, @quantity, @productPrice, @totalPrice); ";

                        sqlcmdProduct.Connection = connection;
                        sqlcmdProduct.Transaction = transaction;
                        sqlcmdProduct.CommandText = queryProducts;
                        sqlcmdProduct.Parameters.AddWithValue("@productId", product.ProductId);
                        sqlcmdProduct.Parameters.AddWithValue("@orderId", id);
                        sqlcmdProduct.Parameters.AddWithValue("@quantity", product.Quantity);
                        sqlcmdProduct.Parameters.AddWithValue("@productPrice", product.ProductPrice);
                        sqlcmdProduct.Parameters.AddWithValue("@totalPrice", product.TotalPrice);

                        await sqlcmdProduct.ExecuteNonQueryAsync();

                    }

                    transaction.Commit();
                    connection.Close();
                    return order;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }


        public async Task<SalesOrder> GetById(int id)
        {
            SqlCommand sqlcmd;
            string query = " SELECT Id, Email, PurchaseDate FROM SalesOrder WHERE Id=@id; ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@id", id.ToString());
                using (var reader = await sqlcmd.ExecuteReaderAsync(System.Data.CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        var salesOrder = new SalesOrder()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal(nameof(SalesOrder.Id))),
                            CustomerEmail = reader.GetString(reader.GetOrdinal("Email")),
                            PurchaseDate = reader.GetDateTime(reader.GetOrdinal(nameof(SalesOrder.PurchaseDate)))
                        };
                        connection.Close();
                        return salesOrder;
                    }
                }
            }
            return default;
        }

        public async Task<List<SalesOrder>> List()
        {
            var orders = new List<SalesOrder>();
            SqlCommand sqlcmd;
            string query = @" SELECT so.Id, so.Email, so.PurchaseDate, SUM(po.TotalPrice) as OrderTotalPrice 
                                 FROM SalesOrder so left join ProductOrder po
                                 ON so.Id = po.OrderId
                                 GROUP BY so.Id, so.Email, so.PurchaseDate; ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.CommandText = query;
                using (var reader = await sqlcmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        orders.Add(new SalesOrder()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal(nameof(SalesOrder.Id))),
                            CustomerEmail = reader.GetString(reader.GetOrdinal("Email")),
                            PurchaseDate = reader.GetDateTime(reader.GetOrdinal(nameof(SalesOrder.PurchaseDate))),
                            OrderTotalPrice = reader.GetDecimal(reader.GetOrdinal(nameof(SalesOrder.OrderTotalPrice)))
                        });
                    }
                }
            }
            return orders;
        }

        public async Task<SalesOrder> Update(SalesOrder order)
        {

            SqlCommand sqlcmd;
            SqlTransaction transaction;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = " UPDATE SalesOrder SET Email = @email WHERE Id = @id; ";
                connection.Open();
                sqlcmd = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                try
                {
                    sqlcmd.Connection = connection;
                    sqlcmd.Transaction = transaction;
                    sqlcmd.CommandText = query;
                    sqlcmd.Parameters.AddWithValue("@id", order.Id);
                    sqlcmd.Parameters.AddWithValue("@email", order.CustomerEmail);
                    await sqlcmd.ExecuteNonQueryAsync();

                    List<ProductOrder> currentProducts = await GetByOrderId((int)order.Id);

                    List<ProductOrder> removedProducts = new List<ProductOrder>();
                    List<ProductOrder> addedProducts = new List<ProductOrder>();
                    List<ProductOrder> updatedProducts = new List<ProductOrder>();

                    foreach (var item in order.ProductOrders)
                    {
                        var productListResult = currentProducts.Any(x => x.Id == item.Id);
                        if (!productListResult)
                            addedProducts.Add(item);
                        else
                            updatedProducts.Add(item);
                    }

                    foreach (var item in currentProducts)
                    {
                        var resultado = order.ProductOrders.Any(x => x.Id == item.Id);
                        if (!resultado)
                            removedProducts.Add(item);
                    }

                    if (addedProducts.Count > 0)
                        AddProductOrder(addedProducts, connection, transaction);
                    if (updatedProducts.Count > 0)
                        UpdateProductOrder(updatedProducts, connection, transaction);
                    if (removedProducts.Count > 0)
                        DeleteProductOrder(removedProducts, connection, transaction);                    

                    transaction.Commit();
                    connection.Close();
                    return order;

                } 
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            
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

        public async Task Delete(int id)
        {
            SqlCommand sqlcmd;
            SqlTransaction transaction;
            string query = @"DELETE FROM ProductOrder WHERE OrderId = @id  
                             DELETE FROM SalesOrder WHERE Id = @id; ";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                try
                {
                    sqlcmd.Connection = connection;
                    sqlcmd.Transaction = transaction;
                    sqlcmd.CommandText = query;
                    sqlcmd.Parameters.AddWithValue("@id", id.ToString());
                    await sqlcmd.ExecuteNonQueryAsync();
                    transaction.Commit();
                    connection.Close();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void AddProductOrder(List<ProductOrder> list, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlcmd = connection.CreateCommand();
            foreach (var item in list)
            {
                var query = @" INSERT INTO ProductOrder (ProductId, OrderId, Quantity, ProductPrice, TotalPrice) 
                            VALUES (@productId, @orderId, @quantity, @productPrice, @totalPrice) ; ";

                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@productId", item.ProductId);
                sqlcmd.Parameters.AddWithValue("@orderId", item.OrderId);
                sqlcmd.Parameters.AddWithValue("@quantity", item.Quantity);
                sqlcmd.Parameters.AddWithValue("@productPrice", item.ProductPrice);
                sqlcmd.Parameters.AddWithValue("@totalPrice", item.TotalPrice);
                sqlcmd.Transaction = transaction;
                sqlcmd.ExecuteNonQueryAsync();
            }
        }

        private void UpdateProductOrder(List<ProductOrder> list, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlcmd = connection.CreateCommand();
            foreach (var item in list)
            {
                var query = @" UPDATE ProductOrder SET Quantity = @quantity, ProductPrice = @productPrice, TotalPrice = @totalPrice 
                            WHERE Id = @Id ; ";

                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@Id", item.Id);
                sqlcmd.Parameters.AddWithValue("@quantity", item.Quantity);
                sqlcmd.Parameters.AddWithValue("@productPrice", item.ProductPrice);
                sqlcmd.Parameters.AddWithValue("@totalPrice", item.TotalPrice);
                sqlcmd.Transaction = transaction;
                sqlcmd.ExecuteNonQueryAsync();
            }
        }

        private void DeleteProductOrder(List<ProductOrder> list, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlcmd = connection.CreateCommand();
            foreach (var item in list)
            {
                var query = @" DELETE ProductOrder WHERE Id = @Id; ";

                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@Id", item.Id);
                sqlcmd.Transaction = transaction;
                sqlcmd.ExecuteNonQueryAsync();
                
            }
        }

    }


}

