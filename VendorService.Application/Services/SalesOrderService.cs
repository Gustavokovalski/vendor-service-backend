using AutoMapper;
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
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IMapper _mapper;
        private readonly ISalesOrderRepository _repository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly SalesOrderModelValidator _salesOrderModelValidator;
        public SalesOrderService(IMapper mapper, ISalesOrderRepository repository, 
            IProductOrderRepository productOrderRepository, 
            SalesOrderModelValidator salesOrderModelValidator)
        {
            _mapper = mapper;
            _repository = repository;
            _productOrderRepository = productOrderRepository;
            _salesOrderModelValidator = salesOrderModelValidator;
        }

        public async Task<BaseModel<SalesOrderModel>> Create(SalesOrderModel orderModel)
        {
            var validator =  _salesOrderModelValidator.Validate(orderModel);
            if (!validator.IsValid)
            {
                return new BaseModel<SalesOrderModel>(false, validator.Errors);
            }
            var order = _mapper.Map<SalesOrder>(orderModel);
            var result = _mapper.Map<SalesOrderModel>(await _repository.Create(order));
            return new BaseModel<SalesOrderModel>(true, EMessages.Success, result);
        }

        public async Task<BaseModel<SalesOrderModel>> Update(SalesOrderModel productModel)
        {
            var entity = await _repository.GetById(productModel.Id.Value);

            if (entity is null)
                return new BaseModel<SalesOrderModel>(false, EMessages.SuccessOrderEdit, _mapper.Map<SalesOrderModel>(entity));

            var product = _mapper.Map<SalesOrder>(productModel);
            var result = _mapper.Map<SalesOrderModel>(await _repository.Update(product));

            return new BaseModel<SalesOrderModel>(true, EMessages.Success, result);
        }

        public async Task<BaseModel<SalesOrderModel>> Delete(int id)
        {
            await _repository.Delete(id);
            return new BaseModel<SalesOrderModel>(true, EMessages.Success);
        }

        public async Task<BaseModel<SalesOrderModel>> GetById(int id)
        {
            var result = _mapper.Map<SalesOrderModel>(await _repository.GetById(id));
            return new BaseModel<SalesOrderModel>(true, EMessages.Success, result);
        }

        public async Task<BaseModel<List<ProductOrderModel>>> GetByOrderId(int id)
        {
            var result = _mapper.Map<List<ProductOrderModel>>(await _productOrderRepository.GetByOrderId(id));
            return new BaseModel<List<ProductOrderModel>>(true, EMessages.Success, result);
        }

        public async Task<BaseModel<List<SalesOrderModel>>> List()
        {
            var result = _mapper.Map<List<SalesOrderModel>>(await _repository.List());
            return new BaseModel<List<SalesOrderModel>>(true, EMessages.Success, result);
        }
    }
}
