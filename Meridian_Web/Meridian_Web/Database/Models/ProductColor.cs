using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class ProductColor : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int ColorId { get; set; }
        public Color? Color { get; set; }

    }
}
