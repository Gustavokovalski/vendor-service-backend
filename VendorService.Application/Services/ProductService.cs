using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Models;
using VendorService.Domain.Repositories;

namespace VendorService.Application.Services.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public ProductService(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<BaseModel<ProductModel>> Create(ProductModel productModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseModel<ProductModel>> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseModel<ProductModel>> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseModel<List<ProductModel>>> List()
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseModel<ProductModel>> Update(ProductModel productModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
