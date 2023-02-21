using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class WishlistProduct : BaseEntity<int>, IAuditable
    {
        public int WishlistId { get; set; }
        public Wishlist? Wishlist { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
