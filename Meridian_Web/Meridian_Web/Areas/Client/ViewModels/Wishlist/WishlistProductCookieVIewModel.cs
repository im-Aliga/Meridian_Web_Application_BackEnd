namespace Meridian_Web.Areas.Client.ViewModels.Wishlist
{
    public class WishlistProductCookieVIewModel
    {
       
        public WishlistProductCookieVIewModel()
        {
            
        }

        public WishlistProductCookieVIewModel(int ıd, string title, string ımageUrl, decimal price, decimal? discountPrice, int quantity)
        {
            Id = ıd;
            Title = title;
            ImageUrl = ımageUrl;
            Price = price;
            DiscountPrice = discountPrice;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int Quantity { get; set; }

    }
}
