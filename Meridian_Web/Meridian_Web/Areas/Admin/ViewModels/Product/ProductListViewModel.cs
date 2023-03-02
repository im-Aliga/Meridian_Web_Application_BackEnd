using Meridian_Web.Areas.Admin.ViewModels.Discount;
using Meridian_Web.Areas.Admin.ViewModels.Brand;

namespace Meridian_Web.Areas.Admin.ViewModels.Product
{
    public class ProductListViewModel
    {
       

        public int Id { get; set; }
        public string Title { get; set; }   
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int InStock { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<BrandViewModel> Brands { get; set;}
        public List<DiscountViewModel> Discounts { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public ProductListViewModel(int ıd, string title, decimal price, decimal? discountPrice, int ınStock, string content, DateTime createdAt, DateTime updatedAt, List<CategoryViewModeL> categories, List<ColorViewModeL> colors, List<BrandViewModel> brands, List<DiscountViewModel> discounts, List<SizeViewModeL> sizes, List<TagViewModel> tags)
        {
            Id = ıd;
            Title = title;
            Price = price;
            DiscountPrice = discountPrice;
            InStock = ınStock;
            Content = content;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Categories = categories;
            Colors = colors;
            Brands = brands;
            Discounts = discounts;
            Sizes = sizes;
            Tags = tags;
        }




        public class BrandViewModel
        {
            public BrandViewModel(string title)
            {
                Title = title;
               
            }

            public string Title { get; set; }
           


        }
        public class DiscountViewModel
        {

            public string Title { get; set; }
            public int DiscontPers { get; set; }
            public DateTime DiscountTime { get; set; }
            public DiscountViewModel(string title, int discontPers, DateTime discountTime)
            {
                Title = title;
                DiscontPers = discontPers;
                DiscountTime = discountTime;
            }
           
        }
        public class CategoryViewModeL
        {
               

            public string Title { get; set; }
            public CategoryViewModeL(string title)
            {
                Title = title;
            }
            


        }
        public class SizeViewModeL
        {
            public SizeViewModeL(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
        public class ColorViewModeL
        {
            public ColorViewModeL(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }
        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
    }
}
