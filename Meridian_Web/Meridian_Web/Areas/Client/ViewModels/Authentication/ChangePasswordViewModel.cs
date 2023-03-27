using System.ComponentModel.DataAnnotations;

namespace Meridian_Web.Areas.Client.ViewModels.Authentication
{
    public class ChangePasswordViewModel
    {
        public string Token { get; set; }
        //[RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{5,}", ErrorMessage = "Your password must be at least 5 characters long and contain at least 1 letter and 1 number")]
        //[DataType(DataType.Password)]
        //[Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[Compare(nameof(Password), ErrorMessage = "Password and confirm password is not same")]
        public string ConfirmPassword { get; set; }
    }
}
