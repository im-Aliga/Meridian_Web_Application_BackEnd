using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Product;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Product
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
                .MinimumLength(2)
                .WithMessage("Minimum length should be 2")
                .MaximumLength(100)
                .WithMessage("Maximum length should be 100");
            RuleFor(avm => avm.Content)
              .NotNull()
              .WithMessage("Content can't be empty")
              .NotEmpty()
              .WithMessage("Content can't be empty")
              .MinimumLength(2)
              .WithMessage("Minimum length should be 2")
              .MaximumLength(10000)
              .WithMessage("Maximum length should be 10000");
            RuleFor(avm => avm.Price)
             .NotNull()
             .WithMessage("Price can't be empty")
             .NotEmpty()
             .WithMessage("Price can't be empty")
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(avm => avm.InStock)
            .NotNull()
            .WithMessage("InStock can't be empty")
            .NotEmpty()
            .WithMessage("InStock can't be empty")
            .GreaterThan(0).WithMessage("InStock must be greater than zero.");
        }

    }
}
