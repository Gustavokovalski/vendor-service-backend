using FluentValidation;
using VendorService.Application.Mappers;

namespace VendorService.Application.Validators
{
    public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterModelValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty()
                 .NotNull()
                 .EmailAddress();

            RuleFor(x => x.ProfileId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(32)
                .WithMessage("Invalid format.");
        }
    }
}
