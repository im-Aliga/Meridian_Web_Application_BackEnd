using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Discount;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Discount
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
            RuleFor(avm => avm.DiscontPers)
               .NotNull()
               .WithMessage("DiscontPers can't be empty")
               .NotEmpty()
               .WithMessage("DiscontPers can't be empty")
               .GreaterThan(1)
               .WithMessage("Minimum should be 1")
               .LessThan(100)
               .WithMessage("Maximum should be 100");
            RuleFor(avm => avm.DiscountTime)
               .NotNull()
               .WithMessage("DiscountTime can't be empty")
               .NotEmpty()
               .WithMessage("DiscountTime can't be empty");
               
        }
    }
    
    
}
