using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class ProductCatagory : BaseEntity<int>, IAuditable
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
