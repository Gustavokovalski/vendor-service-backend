using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VendorService.Domain.Entities;
using VendorService.Domain.Enums;
using VendorService.Domain.Extensions;
using VendorService.Domain.Repositories;

namespace VendorService.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<User> Authenticate(string email, string password)
        {
            SqlCommand sqlcmd;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();

                string query = "SELECT Id, Email, UserProfile FROM UserRegistration WHERE 1=1 AND email = @email AND userpassword = @userpassword; ";

                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@email", email);
                sqlcmd.Parameters.AddWithValue("@userpassword", password);
                using var reader = await sqlcmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new User()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Profile = (EProfiles)reader.GetInt32(reader.GetOrdinal("UserProfile"))
                    };
                }
            }
            return default;
        }

        public async Task<User> Create(User user)
        {
            SqlCommand sqlcmd;
            SqlTransaction transaction;
            string query = " INSERT INTO UserRegistration ( Email, UserProfile, UserPassword ) VALUES (@email, @userprofile, @userpassword); ";
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
                    sqlcmd.Parameters.AddWithValue("@email", user.Email);
                    sqlcmd.Parameters.AddWithValue("@userprofile", user.Profile.GetEnumValue());
                    sqlcmd.Parameters.AddWithValue("@userpassword", user.Password);
                    await sqlcmd.ExecuteScalarAsync();
                    transaction.Commit();
                    connection.Close();
                    user.Password = string.Empty;
                    return user;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<User> GetById(Guid id)
        {

            SqlCommand sqlcmd;
            string query = " SELECT Id, UserProfile, UserProfile FROM UserRegistration WHERE Id=@id; ";

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
                        var user = new User()
                        {
                            Id = reader.GetGuid(reader.GetOrdinal(nameof(User.Id))),
                            Email = reader.GetString(reader.GetOrdinal(nameof(User.Email))),
                            Profile = (EProfiles)reader.GetInt32(reader.GetOrdinal(nameof(User.Profile)))
                        };
                        connection.Close();

                        return user;
                    }
                }
            }
            return default;
        }

        public async Task<List<User>> List()
        {
            var users = new List<User>();
            SqlCommand sqlcmd;
            string query = " SELECT Id, Email, UserProfile FROM UserRegistration; ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.CommandText = query;
                using (var reader = await sqlcmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User()
                        {
                            Id = reader.GetGuid(reader.GetOrdinal(nameof(User.Id))),
                            Email = reader.GetString(reader.GetOrdinal(nameof(User.Email))),
                            Profile = (EProfiles)reader.GetInt32(reader.GetOrdinal("UserProfile"))
                        });
                    }
                }
            }
            return users;
        }


        public async Task<User> Update(User user)
        {
            SqlCommand sqlcmd;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = " UPDATE UserRegistration SET Email = @email, UserProfile = @userProfile, @UserPassword, @userPassword WHERE Id = @id; ";
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@id", user.Id.ToString().ToUpper());
                sqlcmd.Parameters.AddWithValue("@email", user.Email);
                sqlcmd.Parameters.AddWithValue("@profile", user.Profile.GetEnumValue());
                sqlcmd.Parameters.AddWithValue("@password", user.Password);
                await sqlcmd.ExecuteNonQueryAsync();
                connection.Close();
                user.Password = string.Empty;
                return user;
            }
        }

        public async Task Delete(Guid id)
        {
            SqlCommand sqlcmd;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = " DELETE FROM UserRegistration WHERE Id = @id; ";
                connection.Open();
                sqlcmd = connection.CreateCommand();
                sqlcmd.Connection = connection;
                sqlcmd.CommandText = query;
                sqlcmd.Parameters.AddWithValue("@id", id.ToString().ToUpper());
                await sqlcmd.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
    }
}
