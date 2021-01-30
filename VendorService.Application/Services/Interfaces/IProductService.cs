using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Models;

namespace VendorService.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<BaseModel<ProductModel>> Create(ProductModel productModel);
        Task<BaseModel<ProductModel>> Update(ProductModel productModel);
        Task<BaseModel<ProductModel>> Delete(int id);
        Task<BaseModel<ProductModel>> GetById(int id);
        Task<BaseModel<List<ProductModel>>> List();
    }
}
