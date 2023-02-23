using System.ComponentModel.DataAnnotations;

namespace Meridian_Web.Areas.Admin.ViewModels.SubNavbar
{
    public class UpdateViewModel
    {  
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        [Range(1, 10, ErrorMessage = "Value must be between 1 and 10.")]
        public int Order { get; set; }

        public int NavbarId { get; set; }
        public List<NavbarListItemViewModel>? Navbars { get; set; }

    }
}
