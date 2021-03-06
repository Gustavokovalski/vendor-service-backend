﻿using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Models;
using VendorService.Application.Validators;
using VendorService.Domain.Enums;
using VendorService.Domain.Repositories;
using VendorService.Domain.Services.Entities;

namespace VendorService.Application.Services.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly ProductModelValidator _productModelValidator;
        public ProductService(IMapper mapper, IProductRepository repository, ProductModelValidator productModelValidator)
        {
            _mapper = mapper;
            _repository = repository;
            _productModelValidator = productModelValidator;
        }

        public async Task<BaseModel<ProductModel>> Create(ProductModel productModel)
        {
            var validationResult = _productModelValidator.Validate(productModel);
            if (!validationResult.IsValid)
            {
                return new BaseModel<ProductModel>(false, validationResult.Errors);
            }

            var entity = _mapper.Map<Product>(productModel);
            var result = _mapper.Map<ProductModel>(await _repository.Create(entity));
            return new BaseModel<ProductModel>(true, EMessages.Success, result);
        }

        public async Task<BaseModel<ProductModel>> Update(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            var result = _mapper.Map<ProductModel>(await _repository.Update(product));

            return new BaseModel<ProductModel>(true, EMessages.SuccessProductEdit, result);
        }

        public async Task<BaseModel<ProductModel>> Inactivate(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            product.Active = false;
            var result = _mapper.Map<ProductModel>(await _repository.Update(product));
            return new BaseModel<ProductModel>(true, EMessages.SuccessProductDelete, result);
        }

        public async Task<BaseModel<ProductModel>> GetById(int id)
        {   
            var result = _mapper.Map<ProductModel>(await _repository.GetById(id));
            return new BaseModel<ProductModel>(true, EMessages.Success, result);
        }

 
        public async Task<BaseModel<List<ProductModel>>> List()
        {
            var result = _mapper.Map<List<ProductModel>>(await _repository.List());
            return new BaseModel<List<ProductModel>>(true, EMessages.Success, result);
        }

    }
}
