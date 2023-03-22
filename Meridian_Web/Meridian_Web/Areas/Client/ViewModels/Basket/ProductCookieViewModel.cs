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

        public ProductCookieViewModel()
        {
            
        }
        public ProductCookieViewModel(int ıd, string? title, string? ımageUrl, int quantity, decimal price, decimal? discountPrice, decimal total)
        {
            Id = ıd;
            Title = title;
            ImageUrl = ımageUrl;
            Quantity = quantity;
            Price = price;
            DiscountPrice = discountPrice;
            Total = total;
        }
  



     






    }
}
