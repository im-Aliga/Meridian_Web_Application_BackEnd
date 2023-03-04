namespace Meridian_Web.Areas.Admin.ViewModels.Product
{
    public class CatagoryListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public CatagoryListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
