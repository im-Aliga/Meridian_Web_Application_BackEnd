using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class GlobalOffer : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string MainContext { get; set; }
        public string Context { get; set; }
        public string ButtonContext { get; set; }
        public DateTime OfferTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

