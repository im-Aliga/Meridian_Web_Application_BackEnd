using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Discont : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public DateTime DiscountTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProductDiscont> ProductDisconts { get; set; }
    }
}
