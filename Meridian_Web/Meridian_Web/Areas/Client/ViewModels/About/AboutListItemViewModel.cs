namespace Meridian_Web.Areas.Client.ViewModels.About
{
    public class AboutListItemViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public AboutListItemViewModel(string title, string description)
        {
            Title = title;
            Description = description;
        }

    }
}
