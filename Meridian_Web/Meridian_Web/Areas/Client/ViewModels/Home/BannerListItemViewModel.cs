namespace Meridian_Web.Areas.Client.ViewModels.Home
{
    public class BannerListItemViewModel
    {

        public string Title { get; set; }
        public string MainContext { get; set; }
        public string Context { get; set; }
        public string ImageUrl { get; set; }
        public BannerListItemViewModel(string title, string mainContext, string context, string ımageUrl)
        {
            Title = title;
            MainContext = mainContext;
            Context = context;
            ImageUrl = ımageUrl;
        }
       

    }
}
