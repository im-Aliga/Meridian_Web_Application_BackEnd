using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Category : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public List<ProductCatagory> ProductCatagories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
