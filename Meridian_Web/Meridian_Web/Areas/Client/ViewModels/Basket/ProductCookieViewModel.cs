using Meridian_Web.Database.Models;

namespace Meridian_Web.Areas.Client.ViewModels.Basket
{
    public class ProductCookieViewModel
    {

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal Total { get; set; }
        public int? SizeId { get; set; }
        public List<SizeListItemViewModel> Sizes { get; set; }
        public int? ColorId { get; set; }
        public List<ColorListItemViewModel>Colors { get; set; }


        public ProductCookieViewModel()
        {
            
        }

        public ProductCookieViewModel(int ıd, string? title, string? ımageUrl, int quantity, decimal price, decimal? discountPrice, decimal total, int? sizeId, List<SizeListItemViewModel> sizes, int? colorId, List<ColorListItemViewModel> colors)
        {
            Id = ıd;
            Title = title;
            ImageUrl = ımageUrl;
            Quantity = quantity;
            Price = price;
            DiscountPrice = discountPrice;
            Total = total;
            SizeId = sizeId;
            Sizes = sizes;
            ColorId = colorId;
            Colors= colors;
        }
        //public ProductCookieViewModel(int ıd, string? title, string? ımageUrl, int quantity, decimal price, decimal? discountPrice, decimal total)
        //{
        //    Id = ıd;
        //    Title = title;
        //    ImageUrl = ımageUrl;
        //    Quantity = quantity;
        //    Price = price;
        //    DiscountPrice = discountPrice;
        //    Total = total;
        //}











    }
    public class SizeListItemViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public SizeListItemViewModel(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }
        public SizeListItemViewModel()
        {
            
        }
    }
    public class ColorListItemViewModel
    {
        public ColorListItemViewModel(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }
        public ColorListItemViewModel()
        {
            
        }

        public int Id { get; set; } 
        public string Name { get; set; }
    }
}
