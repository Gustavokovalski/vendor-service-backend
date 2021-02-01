using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Models;

namespace VendorService.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseModel<UserModel>> Authenticate(LoginModel loginModel);
        Task<BaseModel<UserRegisterModel>> Create(UserRegisterModel userRegisterModel);
        Task<BaseModel<UserRegisterModel>> Update(UserRegisterModel userRegisterModel);
        Task<BaseModel<UserModel>> Delete(Guid id);
        Task<BaseModel<UserModel>> GetById(Guid id);
        Task<BaseModel<List<UserModel>>> List();
    }
}
