using BackEndFinalProject.Areas.Admin.ViewModels.About;
using FluentValidation;

namespace Meridian_Web.Areas.Admin.Validators.Admin.About
{
    public class UpdateViewModelValidator : AbstractValidator<UpdateViewModel>
    {
        public UpdateViewModelValidator()
        {
            RuleFor(avm => avm.Title)
              .NotNull()
              .WithMessage("Title can't be empty")
              .NotEmpty()
              .WithMessage("Title can't be empty")
              .MinimumLength(5)
              .WithMessage("Minimum length should be 5")
              .MaximumLength(30)
              .WithMessage("Maximum length should be 30");

            RuleFor(avm => avm.Content)
           .NotNull()
           .WithMessage("ToURL can't be empty")
           .NotEmpty()
           .WithMessage("ToURL can't be empty")
           .MinimumLength(10)
           .WithMessage("Minimum length should be 10")
           .MaximumLength(2000)
           .WithMessage("Maximum length should be 2000");
        }
    }
}
