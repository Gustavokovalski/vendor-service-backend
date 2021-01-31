using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VendorService.Application.Mappers;

namespace VendorService.Application.Validators
{
    public class ProductOrderModelValidator : AbstractValidator<ProductOrderModel>
    {
        public ProductOrderModelValidator()
        {
            RuleFor(x => x.ProductPrice)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ProductId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.TotalPrice)
                .NotNull()
                .NotEmpty()
                .Equal(x => x.ProductPrice * x.Quantity);
        }
    }
}
