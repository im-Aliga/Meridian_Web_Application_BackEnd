using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.Payment;

namespace Meridian_Web.Areas.Admin.Validators.Admin.Payment
{
    public class AddViewModelValidator : AbstractValidator<AddPaymentViewModel>
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

            RuleFor(avm => avm.Content)
           .NotNull()
           .WithMessage("Content can't be empty")
           .NotEmpty()
           .WithMessage("Content can't be empty")
           .MinimumLength(2)
           .WithMessage("Minimum length should be 2")
           .MaximumLength(50)
           .WithMessage("Maximum length should be 50");

            RuleFor(f => f.Image)
               .NotNull()
               .WithMessage("Please select a Image.")
               .NotEmpty()
               .WithMessage("Image can't be empty");

            RuleFor(f => f.Image.ContentType)
           .Must(contentType => contentType.Equals("image/jpeg") || contentType.Equals("image/png"))
           .WithMessage("The Image must be in JPEG or PNG format.");

            RuleFor(f => Path.GetExtension(f.Image.FileName).ToLower())
           .Must(extension => extension.Equals(".jpeg") || extension.Equals(".jpg") || extension.Equals(".png"))
           .WithMessage("The file must be in JPEG or PNG format.");
        }
    }
}
