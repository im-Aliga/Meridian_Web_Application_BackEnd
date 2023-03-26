
namespace Meridian_Web.Areas.Client.ViewModels.BlogDetails
{
    public class BlogDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Proverb { get; set; }
        public string ProverbAuthor { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CatagoryViewModeL> Catagories { get; set; }
        public List<TagViewModeL> Tags { get; set; }
        public List<FileViewModeL> Files { get; set; }






        public class FileViewModeL
        {
            public FileViewModeL(string fileUrl, bool isShowVideo, bool isShowImage, bool ısShowAudio)
            {
                FileUrl = fileUrl;
                IsShowVideo = isShowVideo;
                IsShowImage = isShowImage;
                IsShowAudio = ısShowAudio;
            }

            public string FileUrl { get; set; }
            public bool IsShowVideo { get; set; }
            public bool IsShowImage { get; set; }
            public bool IsShowAudio { get; set; }
        }



        public class CatagoryViewModeL
        {
            public CatagoryViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class TagViewModeL
        {
            public TagViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }



        

    }
}
