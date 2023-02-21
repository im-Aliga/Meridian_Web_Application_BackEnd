using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Color : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProductColor> PlantColors { get; set; }

    }
}
