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
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Product> Create(Product product)
        {
            SqlCommand sqlcmd;
            SqlTransaction transaction;
            string query = " INSERT INTO Product ( Name, Price, Active ) VALUES (@name, @price, @active); ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                try
                {
                    sqlcmd.Connection = connection;
                    sqlcmd.Transaction = transaction;
                    sqlcmd.CommandText = query;
                    sqlcmd.Parameters.AddWithValue("@name", product.Name);
                    sqlcmd.Parameters.AddWithValue("@price", product.Price);
                    sqlcmd.Parameters.AddWithValue("@active", product.Active);
                    await sqlcmd.ExecuteScalarAsync();
                    transaction.Commit();
                    connection.Close();
                    return product;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<Product> GetById(int id)
        {
            SqlCommand sqlcmd;
            string query = " SELECT Id, Name, Price, Active FROM Product WHERE Id=@id; ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@id", id.ToString().ToUpper());
                using (var reader = await sqlcmd.ExecuteReaderAsync(System.Data.CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        var product = new Product()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal(nameof(Product.Id))),
                            Name = reader.GetString(reader.GetOrdinal(nameof(Product.Name))),
                            Price = reader.GetDecimal(reader.GetOrdinal(nameof(Product.Price))),
                            Active = reader.GetBoolean(reader.GetOrdinal(nameof(Product.Active)))
                        };
                        connection.Close();
                        return product;
                    }
                }
            }
            return default;
        }

        public async Task<List<Product>> List()
        {
            var products = new List<Product>();
            SqlCommand sqlcmd;
            string query = " SELECT Id, Name, Price, Active FROM Product; ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.CommandText = query;
                using (var reader = await sqlcmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal(nameof(Product.Id))),
                            Name = reader.GetString(reader.GetOrdinal(nameof(Product.Name))),
                            Price = reader.GetDecimal(reader.GetOrdinal(nameof(Product.Price))),
                            Active = reader.GetBoolean(reader.GetOrdinal(nameof(Product.Active)))
                        });
                    }
                }
            }
            return products;
        }

        public async Task<Product> Update(Product product)
        {
            SqlCommand sqlcmd;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = " UPDATE Product SET Name = @name, Price = @price, Active = @active WHERE Id = @id; ";
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@id", product.Id);
                sqlcmd.Parameters.AddWithValue("@name", product.Name);
                sqlcmd.Parameters.AddWithValue("@price", product.Price);
                sqlcmd.Parameters.AddWithValue("@active", product.Active);
                await sqlcmd.ExecuteNonQueryAsync();
                connection.Close();
                return product;
            }
        }
        //public async Task Inactivate(int id)
        //{
        //    SqlCommand sqlcmd;
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        string query = " DELETE FROM Product WHERE Id = @id; ";
        //        connection.Open();
        //        sqlcmd = connection.CreateCommand();
        //        sqlcmd.Connection = connection;
        //        sqlcmd.CommandText = query;
        //        sqlcmd.Parameters.AddWithValue("@id", id.ToString().ToUpper());
        //        await sqlcmd.ExecuteNonQueryAsync();
        //        connection.Close();
        //    }
        //}
    }
}
