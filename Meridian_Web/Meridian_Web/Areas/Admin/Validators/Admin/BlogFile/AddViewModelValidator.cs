using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.BlogFile;

namespace Meridian_Web.Areas.Admin.Validators.Admin.BlogFile
{
    public class AddViewModelValidator : AbstractValidator<BlogFileAddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(f => f.File)
           .NotNull().WithMessage("Please select a file.")
           .NotEmpty().WithMessage("file can't be empty");
           //.Must(f => f.Length >= 0).WithMessage("The file is empty.")
           //.Must(file => file.Length <= 1024 * 1024 * 100).WithMessage("The file size must not exceed 100 MB.");

            RuleFor(model => model)
          .Must(HaveExactlyOneCheckboxSelected).WithMessage("Please select exactly one checkbox.");


        }
            private bool HaveExactlyOneCheckboxSelected(BlogFileAddViewModel model)
            {
                int checkedCount = 0;

                if (model.IsShowImage) checkedCount++;
                if (model.IsShowVideo) checkedCount++;
                if (model.IsShowAudio) checkedCount++;

                return checkedCount == 1;
            }
    }
}
