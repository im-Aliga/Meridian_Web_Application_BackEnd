using FluentValidation;
using Meridian_Web.Areas.Admin.ViewModels.User;
using Meridian_Web.Database;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Validators.Admin.User
{
    public class UserAddViewModelValidator : AbstractValidator<AddUserViewModel>
    {
        private readonly DataContext _dataContext;
        public UserAddViewModelValidator( DataContext dataContext)
        {
            _dataContext=dataContext;

            RuleFor(avm => avm.FirstName)
            .NotNull()
            .WithMessage("FirstName can't be empty")
            .NotEmpty()
            .WithMessage("FirstName can't be empty")
            .MinimumLength(2)
            .WithMessage("FirstName length should be 2")
            .MaximumLength(20)
            .WithMessage("FirstName length should be 20");

            RuleFor(avm => avm.LastName)
             .NotNull()
            .WithMessage("LastName can't be empty")
            .NotEmpty()
            .WithMessage("LastName can't be empty")
            .MinimumLength(2)
            .WithMessage("LastName length should be 2")
            .MaximumLength(20)
            .WithMessage("LastName length should be 20");

            RuleFor(avm => avm.Address.City)
            .NotNull()
            .WithMessage("City can't be empty")
            .NotEmpty()
            .WithMessage("City can't be empty")
            .MinimumLength(2)
            .WithMessage("Minimum length should be 2")
            .MaximumLength(50)
            .WithMessage("Maximum length should be 50");

            RuleFor(avm => avm.Address.Address)
            .NotNull()
            .WithMessage("Address can't be empty")
            .NotEmpty()
            .WithMessage("Address can't be empty")
            .MinimumLength(2)
            .WithMessage("Minimum length should be 2")
            .MaximumLength(50)
            .WithMessage("Maximum length should be 50");

            RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches("[0-9]").WithMessage("Password must contain at least one number.")
           .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match.")
                .When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Password is required.");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address format.")
            .Must(BeUniqueEmail).WithMessage("Email address is already in use.");
        }

        private bool BeUniqueEmail(string email)
        {
            return !_dataContext.Users.Any(u => u.Email == email);
        }
    }
}
