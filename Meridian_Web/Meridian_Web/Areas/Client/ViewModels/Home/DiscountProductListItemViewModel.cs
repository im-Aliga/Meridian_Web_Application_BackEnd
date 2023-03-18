namespace Meridian_Web.Areas.Client.ViewModels.Home
{
    public class DiscountProductListItemViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public List<CategoriesList> Categories { get; set; }
        public List<DiscountLIst> Discounts { get; set; }
        public string ImageUrl { get; set; }
        public DiscountProductListItemViewModel(int ıd, string title, decimal price, decimal? discountPrice, List<CategoriesList> categories, List<DiscountLIst> discounts, string ımageUrl)
        {
            Id = ıd;
            Title = title;
            Price = price;
            DiscountPrice = discountPrice;
            Categories = categories;
            Discounts = discounts;
            ImageUrl = ımageUrl;
        }

        public class CategoriesList
        {

            public string Title { get; set; }
            public CategoriesList(string title)
            {
                Title = title;
            }
        }

    }
    public class DiscountLIst
    {

        public DateTime DiscountTime { get; set; }
        public DiscountLIst(DateTime discountTime)
        {
            DiscountTime = discountTime;
        }
    }
   
}
