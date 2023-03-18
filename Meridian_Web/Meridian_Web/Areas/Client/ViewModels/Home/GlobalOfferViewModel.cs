namespace Meridian_Web.Areas.Client.ViewModels.Home
{
    public class GlobalOfferViewModel
    {

        public string Title { get; set; }
        public string MainContext { get; set; }
        public string Context { get; set; }
        public string ButtonContext { get; set; }
        public DateTime OfferTime { get; set; }
        public GlobalOfferViewModel(string title, string mainContext, string context, string buttonContext, DateTime offerTime)
        {
            Title = title;
            MainContext = mainContext;
            Context = context;
            ButtonContext = buttonContext;
            OfferTime = offerTime;
        }
    }
}
