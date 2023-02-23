using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Slider;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Slider
{
    public class AddSliderViewModelValidator : AbstractValidator<AddSliderViewModel>
    {
        public AddSliderViewModelValidator()
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

                RuleFor(avm => avm.OfferContext)
               .NotNull()
               .WithMessage("OfferContext can't be empty")
               .NotEmpty()
               .WithMessage("OfferContext can't be empty")
               .MinimumLength(10)
               .WithMessage("Minimum length should be 10")
               .MaximumLength(70)
               .WithMessage("Maximum length should be 70");

                RuleFor(avm => avm.Content)
               .NotNull()
               .WithMessage("Content can't be empty")
               .NotEmpty()
               .WithMessage("Content can't be empty")
               .MinimumLength(10)
               .WithMessage("Minimum length should be 10")
               .MaximumLength(200)
               .WithMessage("Maximum length should be 200");

                RuleFor(avm => avm.StartPrice)
               .NotNull()
               .WithMessage("Content can't be empty")
               .NotEmpty()
               .WithMessage("Content can't be empty")
               .MinimumLength(1)
               .WithMessage("Minimum length should be 1")
               .MaximumLength(7)
               .WithMessage("Maximum length should be 7");

                RuleFor(avm => avm.ButtonName)
               .NotNull()
               .WithMessage("ButtonName can't be empty")
               .NotEmpty()
               .WithMessage("ButtonName can't be empty")
               .MinimumLength(2)
               .WithMessage("Minimum length should be 2")
               .MaximumLength(15)
               .WithMessage("Maximum length should be 15");

                RuleFor(avm => avm.ButtonRedirectUrl)
               .NotNull()
               .WithMessage("ButtonRedirectUrl can't be empty")
               .NotEmpty()
               .WithMessage("ButtonRedirectUrl can't be empty")
               .MinimumLength(10)
               .WithMessage("Minimum length should be 10")
               .MaximumLength(100)
               .WithMessage("Maximum length should be 100");

                RuleFor(avm => avm.Order)
               .NotNull()
               .WithMessage("Order can't be empty")
               .NotEmpty()
               .WithMessage("Order can't be empty")
               .GreaterThan(0)
               .WithMessage("Minimum should be 1")
               .LessThan(10)
               .WithMessage("Maximum should be 10");


        }
    }
}
