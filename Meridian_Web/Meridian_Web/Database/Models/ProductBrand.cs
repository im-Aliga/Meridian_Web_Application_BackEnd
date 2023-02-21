using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class ProductBrand : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }


    }
}
