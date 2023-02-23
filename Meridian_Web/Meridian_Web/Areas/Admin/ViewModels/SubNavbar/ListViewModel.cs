namespace Meridian_Web.Areas.Admin.ViewModels.SubNavbar
{
    public class ListViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Url { get; set; }
        public string Navbar { get; set; }
        public ListViewModel(int ıd, string title, int order, DateTime createdAt, DateTime updatedAt, string url, string navbar)
        {
            Id = ıd;
            Title = title;
            Order = order;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Url = url;
            Navbar = navbar;
        }

       
    }
}
