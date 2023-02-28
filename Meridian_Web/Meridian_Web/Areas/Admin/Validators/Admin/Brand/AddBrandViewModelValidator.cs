using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Brand;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Brand
{
    public class AddViewModelValidator : AbstractValidator<AddBrandViewModel>
    {
        public AddViewModelValidator()
        {
                  RuleFor(avm => avm.Name)
                 .NotNull()
                 .WithMessage("Title can't be empty")
                 .NotEmpty()
                 .WithMessage("Title can't be empty")
                 .MinimumLength(2)
                 .WithMessage("Minimum length should be 2")
                 .MaximumLength(40)
                 .WithMessage("Maximum length should be 40");
                  RuleFor(avm => avm.Image)
                 .NotNull()
                 .WithMessage("İmage can't be empty")
                 .NotEmpty()
                 .WithMessage("İmage can't be empty");

        }
    }
}
