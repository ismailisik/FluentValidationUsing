using FluentValidation;
using FluentValidationUsing.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidationUsing.Web.Validators
{

    public class AddressValidator : AbstractValidator<Address>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alanı zorunludur.";
        public AddressValidator()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(x => x.Country).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(x => x.DoorNumber).NotEmpty().WithMessage(NotEmptyMessage).Must(x =>
            {
                return 1000 > x && x > 0;
            }).WithMessage("{PropertyName} alanı 1-1000 arasında olmalıdır.");

            RuleFor(x => x.Neighborhood).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(x => x.PostalCode).NotEmpty().WithMessage(NotEmptyMessage).MaximumLength(5).WithMessage("{PropertyName} alanı 5 haneli olmalıdır.").MinimumLength(5).WithMessage("{PropertyName} alanı 5 haneli olmalıdır.");
        }
    }
}
