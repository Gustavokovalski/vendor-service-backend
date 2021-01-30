using FluentValidation;
using VendorService.Application.Mappers;

namespace VendorService.Application.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(x => x.Name)
                 .NotEmpty()
                 .NotNull();

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Active)
                .NotEmpty()
                .NotNull();
        }
    }
}
