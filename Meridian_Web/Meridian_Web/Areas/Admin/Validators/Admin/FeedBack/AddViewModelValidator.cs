using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.FeedBack;

namespace Meridian_Web.Areas.Admin.Validators.Admin.FeedBack
{
    public class AddViewModelValidator : AbstractValidator<AddFeedbackViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.FullName)
           .NotNull()
           .WithMessage("FullName can't be empty")
           .NotEmpty()
           .WithMessage("FullName can't be empty")
           .MinimumLength(2)
           .WithMessage("Minimum length should be 2")
           .MaximumLength(40)
           .WithMessage("Maximum length should be 40");

            RuleFor(avm => avm.Context)
           .NotNull()
           .WithMessage("Context can't be empty")
           .NotEmpty()
           .WithMessage("Context can't be empty")
           .MinimumLength(2)
           .WithMessage("Minimum length should be 2")
           .MaximumLength(200)
           .WithMessage("Maximum length should be 200");

            RuleFor(avm => avm.Role)
           .NotNull()
           .WithMessage("Role can't be empty")
           .NotEmpty()
           .WithMessage("Role can't be empty")
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
