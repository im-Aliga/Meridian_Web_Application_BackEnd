using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class UserAddress : BaseEntity<int>, IAuditable
    {
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
