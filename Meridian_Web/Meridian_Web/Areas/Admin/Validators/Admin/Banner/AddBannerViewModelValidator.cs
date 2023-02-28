using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Banner;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Banner
{
    public class AddViewModelValidator : AbstractValidator<AddBannerViewModel>
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
                 .MaximumLength(40)
                 .WithMessage("Maximum length should be 40");

                 RuleFor(avm => avm.MainContext)
                .NotNull()
                .WithMessage("ToURL can't be empty")
                .NotEmpty()
                .WithMessage("ToURL can't be empty")
                .MinimumLength(2)
                .WithMessage("Minimum length should be 2")
                .MaximumLength(50)
                .WithMessage("Maximum length should be 50");

                 RuleFor(avm => avm.Content)
                .NotNull()
                .WithMessage("ToURL can't be empty")
                .NotEmpty()
                .WithMessage("ToURL can't be empty")
                .MinimumLength(2)
                .WithMessage("Minimum length should be 2")
                .MaximumLength(50)
                .WithMessage("Maximum length should be 50");

        }
    }
}
