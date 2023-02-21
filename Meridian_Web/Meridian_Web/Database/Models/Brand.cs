using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Brand : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string PhoteImageName { get; set; }
        public string PhoteInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
       
    }
}
