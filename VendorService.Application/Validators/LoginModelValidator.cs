using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VendorService.Application.Mappers;

namespace VendorService.Application.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(c => c)
                   .NotNull()
                   .OnAnyFailure(x =>
                   {
                       throw new ArgumentNullException("Can't found the object.");
                   });

            RuleFor(c => c.Email).NotNull().EmailAddress();
            RuleFor(c => c.Password).NotNull().MaximumLength(32).WithMessage("Invalid password format.");
        }

    }
}
