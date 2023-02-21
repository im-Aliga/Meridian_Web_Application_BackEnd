using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class FeedBack : BaseEntity<int>, IAuditable
    {
        public string FullName { get; set; }
        public string Context { get; set; }
        public string Role { get; set; }
        public string ProfilePhoteImageName { get; set; }
        public string ProfilePhoteInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
