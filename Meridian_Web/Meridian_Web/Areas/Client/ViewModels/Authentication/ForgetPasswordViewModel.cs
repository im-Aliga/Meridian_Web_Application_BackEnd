
using System.ComponentModel.DataAnnotations;

namespace Meridian_Web.Areas.Client.ViewModels.Authentication
{
    public class ForgetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
