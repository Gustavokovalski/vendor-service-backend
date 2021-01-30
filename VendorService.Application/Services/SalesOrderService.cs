using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Models;
using VendorService.Domain.Enums;
using VendorService.Domain.Repositories;
using VendorService.Domain.Services.Entities;

namespace VendorService.Application.Services.Interfaces
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IMapper _mapper;
        private readonly ISalesOrderRepository _repository;
        public SalesOrderService(IMapper mapper, ISalesOrderRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<BaseModel<SalesOrderModel>> Create(SalesOrderModel orderModel)
        {
            //var validator = await new ProdutoModelValidator().ValidateAsync(produtoModel);
            //if (!validator.IsValid)
            //{
            //    return new BaseModel<ProdutoModel>(false, validator.Errors);
            //}

            var order = _mapper.Map<SalesOrder>(orderModel);
            var result = _mapper.Map<SalesOrderModel>(await _repository.Create(order));
            return new BaseModel<SalesOrderModel>(true, EMessages.Teste, result);
        }

        public Task<BaseModel<SalesOrderModel>> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseModel<SalesOrderModel>> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseModel<List<SalesOrderModel>>> List()
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseModel<SalesOrderModel>> Update(SalesOrderModel salesOrderModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
