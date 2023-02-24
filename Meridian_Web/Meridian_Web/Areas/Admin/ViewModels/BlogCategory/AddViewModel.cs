namespace Meridian_Web.Areas.Admin.ViewModels.BlogCategory
{
    public class AddViewModel
    {
        public string Title { get; set; }
       

        public AddViewModel()
        {

        }

        public AddViewModel(string title)
        {
            Title = title;
           
        }
    }
}
