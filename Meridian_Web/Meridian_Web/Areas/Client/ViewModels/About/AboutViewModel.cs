using Meridian_Web.Areas.Client.ViewModels.Home;

namespace Meridian_Web.Areas.Client.ViewModels.About
{
    public class AboutViewModel
    {
        public List<AboutListItemViewModel> Abouts { get; set; }
        public List<OurTeamMemberLIstItemViewModel> OurTeamMembers { get; set;}
        public List<FeedBackListItemViewModel> FeedBacks { get; set; }
    }
}
