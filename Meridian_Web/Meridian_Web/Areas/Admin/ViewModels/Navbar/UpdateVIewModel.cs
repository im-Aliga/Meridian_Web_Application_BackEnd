using System.ComponentModel.DataAnnotations;

namespace Meridian_Web.Areas.Admin.ViewModels.Navbar
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Range(1, 10, ErrorMessage = "Value must be between 1 and 10.")]
        public int Order { get; set; }
        public string Url { get; set; }
        public bool IsShowHeader { get; set; }
        public bool IsShowFooter { get; set; }
    }
}
