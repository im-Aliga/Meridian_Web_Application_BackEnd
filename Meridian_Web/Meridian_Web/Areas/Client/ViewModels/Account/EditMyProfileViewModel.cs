using System.ComponentModel.DataAnnotations;

namespace Meridian_Web.Areas.Client.ViewModels.Account
{
    public class EditMyProfileViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = " Current Password is required")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password is not same")]
        public string ConfirmPassword { get; set; }
    }
}
