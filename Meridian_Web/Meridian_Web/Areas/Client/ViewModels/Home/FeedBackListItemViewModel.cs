namespace Meridian_Web.Areas.Client.ViewModels.Home
{
    public class FeedBackListItemViewModel
    {

        public string FullName { get; set; }
        public string Context { get; set; }
        public string Role { get; set; }
        public string ImgUrl { get; set; }
        public FeedBackListItemViewModel(string fullName, string context, string role, string ımgUrl)
        {
            FullName = fullName;
            Context = context;
            Role = role;
            ImgUrl = ımgUrl;
        }
    }
}
