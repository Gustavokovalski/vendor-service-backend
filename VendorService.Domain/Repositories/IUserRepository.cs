using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Domain.Entities;

namespace VendorService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string email, string password);
        Task<User> Create(User user);
        Task<User> GetById(Guid id);
        Task<List<User>> List();
        Task<User> Update(User user);
        Task Delete(Guid id);
    }
}
