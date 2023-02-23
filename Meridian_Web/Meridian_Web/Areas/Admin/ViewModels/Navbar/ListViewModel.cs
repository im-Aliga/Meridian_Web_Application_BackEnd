namespace Meridian_Web.Areas.Admin.ViewModels.Navbar
{
    public class ListViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsShowHeader { get; set; }
        public bool IsShowFooter { get; set; }
      
        public ListViewModel(int ıd, string title, int order, string url, DateTime createdAt, DateTime updatedAt, bool ısShowHeader, bool ısShowFooter)
        {
            Id = ıd;
            Title = title;
            Order = order;
            Url = url;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsShowHeader = ısShowHeader;
            IsShowFooter = ısShowFooter;
        }

       


    }
}
