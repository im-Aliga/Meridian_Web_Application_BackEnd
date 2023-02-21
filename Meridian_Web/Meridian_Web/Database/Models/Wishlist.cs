using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Wishlist : BaseEntity<int>, IAuditable
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<WishlistProduct>? WishlistProducts { get; set; }
    }
}
