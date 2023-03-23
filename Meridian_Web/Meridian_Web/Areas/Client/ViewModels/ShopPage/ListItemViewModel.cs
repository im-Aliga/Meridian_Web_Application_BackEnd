namespace Meridian_Web.Areas.Client.ViewModels.ShopPage
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImgUrl { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<BrandViewModel> Brands { get; set; }


       


        public ListItemViewModel() { }

        public ListItemViewModel(int id, string name, decimal price, DateTime createdAt, string imgUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            CreatedAt = createdAt;
            ImgUrl = imgUrl;

        }

        public ListItemViewModel(int ıd, string name, string description, decimal price, decimal? discountPrice, DateTime createdAt, string ımgUrl, List<CategoryViewModeL> categories, List<ColorViewModeL> colors, List<SizeViewModeL> sizes, List<TagViewModel> tags, List<BrandViewModel> brands)
        {
            Id = ıd;
            Name = name;
            Description = description;
            Price = price;
            DiscountPrice = discountPrice;
            CreatedAt = createdAt;
            ImgUrl = ımgUrl;
            Categories = categories;
            Colors = colors;
            Sizes = sizes;
            Tags = tags;
            Brands = brands;
        }

        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
           


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
        public class BrandViewModel
        {
            public string Name { get; set; }
            public BrandViewModel(string name)
            {
                Name = name;
            }
        }
    }
}
