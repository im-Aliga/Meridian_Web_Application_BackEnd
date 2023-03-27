using Meridian_Web.Database.Models;
using Meridian_Web.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class PasswordForget : BaseEntity<int>, IAuditable
    {
        public string? ActivationUrl { get; set; }
        public string? ActivationToken { get; set; }
        public DateTime ExpiredDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
