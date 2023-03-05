using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.GlobalOffer;

namespace Meridian_Web.Areas.Admin.Validators.Admin.GlobalOffer
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Title)
            .NotNull()
            .WithMessage("Title can't be empty")
            .NotEmpty()
            .WithMessage("Title can't be empty")
            .MinimumLength(1)
            .WithMessage("Minimum length should be 1")
            .MaximumLength(40)
            .WithMessage("Maximum length should be 40");

            RuleFor(avm => avm.MainContext)
            .NotNull()
            .WithMessage("MainContext can't be empty")
            .NotEmpty()
            .WithMessage("MainContext can't be empty")
            .MinimumLength(1)
            .WithMessage("Minimum length should be 1")
            .MaximumLength(100)
            .WithMessage("Maximum length should be 100");

            RuleFor(avm => avm.Context)
            .NotNull()
            .WithMessage("Context can't be empty")
            .NotEmpty()
            .WithMessage("Context can't be empty")
            .MinimumLength(1)
            .WithMessage("Context length should be 1")
            .MaximumLength(10000)
            .WithMessage("Context length should be 10000");

             RuleFor(avm => avm.OfferTime)
             .NotNull()
             .WithMessage("DiscountTime can't be empty")
             .NotEmpty()
             .WithMessage("DiscountTime can't be empty");
        }


    }
}
