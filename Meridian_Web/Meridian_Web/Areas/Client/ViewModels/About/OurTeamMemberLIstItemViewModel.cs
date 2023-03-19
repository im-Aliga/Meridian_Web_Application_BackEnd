namespace Meridian_Web.Areas.Client.ViewModels.About
{
    public class OurTeamMemberLIstItemViewModel
    {

        public string FullName { get; set; }
        public string Position { get; set; }
        public string ImgUrl { get; set; }
        public OurTeamMemberLIstItemViewModel(string fullName, string position, string ımgUrl)
        {
            FullName = fullName;
            Position = position;
            ImgUrl = ımgUrl;
        }
    }
}
