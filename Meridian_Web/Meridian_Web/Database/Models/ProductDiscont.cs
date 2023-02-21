using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class ProductDiscont : BaseEntity<int>, IAuditable
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int DiscontId { get; set; }
        public Discont? Discont { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
