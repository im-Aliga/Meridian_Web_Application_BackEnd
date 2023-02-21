using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class BlogFile : BaseEntity<int>, IAuditable
    {
        public string? FileName { get; set; }
        public string? FileNameInFileSystem { get; set; }
        public bool IsShowImage { get; set; }
        public bool IsShowVideo{ get; set; }
        public bool IsShowAudio { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
    }
}
