

using Meridian_Web.Areas.Client.ViewModels.Home;

namespace Meridian_Web.Areas.Client.ViewModels.ProductDetails
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int InStock { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<ImageViewModeL> Images { get; set; }
        public List<DiscountList> Discounts { get; set; }
        public List<ProductListItemViewModel> Products{get;set;}







        public class ImageViewModeL
        {
            public ImageViewModeL(string imageUrl)
            {
                ImageUrl = imageUrl;
            }
            public string ImageUrl { get; set; }
        }



        public class SizeViewModeL
        {
            public SizeViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class ColorViewModeL
        {
            public ColorViewModeL(string name, int id)
            {
                Name = name;
                Id = id;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class DiscountList
        {
            public DiscountList(int ıd, int percentage)
            {
                Id = ıd;
                Percentage = percentage;
            }

            public int Id { get; set; }
            public int Percentage { get; set; }
        }

    }
}
