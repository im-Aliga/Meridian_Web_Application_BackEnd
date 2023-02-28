namespace Meridian_Web.Areas.Admin.ViewModels.BlogFile
{
    public class BlogFileViewModel
    {
        public int Blogİd { get; set; }
        public List<ListItem>? Files { get; set; }


        public class ListItem
        {
            public int Id { get; set; }
            public string? FileUrl { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool IsShowImage { get; set; }
            public bool IsShowVideo { get; set; }
            public bool IsShowAudio { get; set; }
        }
    }
}
