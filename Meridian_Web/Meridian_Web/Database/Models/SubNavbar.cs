using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class SubNavbar : BaseNavbarAndSubNavbar, IAuditable
    {
       

        public int NavbarId { get; set; }
        public Navbar Navbar { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
