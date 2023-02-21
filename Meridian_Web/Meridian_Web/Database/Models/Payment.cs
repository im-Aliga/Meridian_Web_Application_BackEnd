using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Payment : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public string ImageName { get; set; }
        public string ImageNameInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
