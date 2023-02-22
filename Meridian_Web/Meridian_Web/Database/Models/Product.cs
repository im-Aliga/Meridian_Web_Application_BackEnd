using BackEndFinalProject.Database.Models;
using Meridian_Web.Database.Models.Common;


namespace Meridian_Web.Database.Models
{
    public class Product:BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string DiscountPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int InStock { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<BasketProduct>? BasketProducts { get; set; }
        public List<WishlistProduct>? WishlisttProducts { get; set; }
        public List<ProductTag>? ProductTags { get; set; }
        public List<ProductCatagory>? ProductCatagories { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
        public List<ProductColor>? ProductColors { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
        public List<ProductDiscont>? ProductDisconts { get; set; }
        public List<ProductBrand>? ProductBrands { get; set; }





    }
}
