using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Size : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        List<ProductSize> ProductSizes { get; set; }


    }
}
