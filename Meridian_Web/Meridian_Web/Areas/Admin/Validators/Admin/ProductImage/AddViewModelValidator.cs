using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.ProductImage;

namespace Meridian_Web.Areas.Admin.Validators.Admin.ProductImage
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>

    {
        public AddViewModelValidator()
        {

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
