using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class TeamMember :BaseEntity<int>, IAuditable
    {
        public string BgImageName { get; set; }
        public string BgImageNameInFileSystem { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
