using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Size;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Size
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {

        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Name)
            .NotNull()
            .WithMessage("Title can't be empty")
            .NotEmpty()
            .WithMessage("Title can't be empty")
            .MinimumLength(1)
            .WithMessage("Minimum length should be 1")
            .MaximumLength(40)
            .WithMessage("Maximum length should be 40");
        }
    }
}
