using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Blog;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Blog
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
                 .MaximumLength(100)
                 .WithMessage("Maximum length should be 100");

                 RuleFor(avm => avm.Description)
                .NotNull()
                .WithMessage("ToURL can't be empty")
                .NotEmpty()
                .WithMessage("ToURL can't be empty")
                .MinimumLength(2)
                .WithMessage("Minimum length should be 2")
                .MaximumLength(100000)
                .WithMessage("Maximum length should be 100000");

                 RuleFor(avm => avm.Proverb)
                .NotNull()
                .WithMessage("ToURL can't be empty")
                .NotEmpty()
                .WithMessage("ToURL can't be empty")
                .MinimumLength(2)
                .WithMessage("Minimum length should be 2")
                .MaximumLength(200)
                .WithMessage("Maximum length should be 200");

                RuleFor(avm => avm.ProverbAuthor)
               .NotNull()
               .WithMessage("ToURL can't be empty")
               .NotEmpty()
               .WithMessage("ToURL can't be empty")
               .MinimumLength(2)
               .WithMessage("Minimum length should be 2")
               .MaximumLength(200)
               .WithMessage("Maximum length should be 200");

        }
    }
}
