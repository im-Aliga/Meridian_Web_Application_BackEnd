using System.ComponentModel;

namespace Meridian_Web.Areas.Admin.ViewModels.BlogFile
{
    public class BlogFileAddViewModel
    {
       
        public IFormFile File { get; set; }

        public bool IsShowImage { get; set; }
        public bool IsShowVideo { get; set; }
        public bool IsShowAudio { get; set; }
    }
}
