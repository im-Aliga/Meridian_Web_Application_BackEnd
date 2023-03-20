namespace Meridian_Web.Areas.Client.ViewModels.Contact
{
    public class ContactInformationListItemViewModel
    {

        public string Title { get; set; }
        public string MainContext { get; set; }
        public string Context { get; set; }
        public string ImgUrl { get; set; }
        public ContactInformationListItemViewModel(string title, string mainContext, string context, string ımgUrl)
        {
            Title = title;
            MainContext = mainContext;
            Context = context;
            ImgUrl = ımgUrl;
        }
    }
}
