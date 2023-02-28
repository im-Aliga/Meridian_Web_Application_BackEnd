using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.BlogTag;

namespace Meridian_Web.Areas.Admin.Validators.Admin.BlogTag
{
    public class UpdateViewModelValidator : AbstractValidator<UpdateViewModel>
    {
        public UpdateViewModelValidator()
        {
            RuleFor(avm => avm.TagName)
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
