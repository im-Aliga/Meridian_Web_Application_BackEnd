namespace Meridian_Web.Areas.Admin.ViewModels.SubNavbar
{
    public class NavbarListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public NavbarListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
