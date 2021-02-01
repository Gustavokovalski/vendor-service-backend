using FluentValidation;
using VendorService.Application.Mappers;

namespace VendorService.Application.Validators
{
    public class SalesOrderModelValidator : AbstractValidator<SalesOrderModel>
    {
        public SalesOrderModelValidator()
        {
            RuleFor(x => x.CustomerEmail)
                 .NotEmpty()
                 .NotNull()
                 .EmailAddress();

            RuleFor(x => x.PurchaseDate)
                .NotEmpty()
                .NotNull();

            //RuleForEach(x => x.ProductOrders)
            //    .SetValidator(x => new ProductModelValidator());
        }
    }
}
