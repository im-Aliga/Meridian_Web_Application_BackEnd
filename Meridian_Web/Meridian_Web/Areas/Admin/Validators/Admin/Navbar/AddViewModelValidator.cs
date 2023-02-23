
using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Navbar;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Navbar
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
                .MinimumLength(2)
                .WithMessage("Minimum length should be 2")
                .MaximumLength(10)
                .WithMessage("Maximum length should be 10");

            RuleFor(avm => avm.Url)
           .NotNull()
           .WithMessage("ToURL can't be empty")
           .NotEmpty()
           .WithMessage("ToURL can't be empty")
           .MinimumLength(10)
           .WithMessage("Minimum length should be 10")
           .MaximumLength(50)
           .WithMessage("Maximum length should be 50");

           
        }
    }
}
