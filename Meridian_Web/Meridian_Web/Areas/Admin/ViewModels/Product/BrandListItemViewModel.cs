namespace Meridian_Web.Areas.Admin.ViewModels.Product
{
    public class BrandListItemViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public BrandListItemViewModel(int ıd, string title)
        {
            Id = ıd;
            Title = title;
        }

    }
}
