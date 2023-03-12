
namespace BackEndFinalProject.Areas.Client.ViewModels.Home.Modal
{
    public class ModalViewModel
    {
        public ModalViewModel(int ıd, string title, string description, decimal price, decimal? discountPrice, int ınStock, string ımgUrl, List<DiscountList>? discounts)
        {
            Id = ıd;
            Title = title;
            Description = description;
            Price = price;
            DiscountPrice = discountPrice;
            InStock = ınStock;
            ImgUrl = ımgUrl;
            Discounts = discounts;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int InStock { get; set; }
        public string ImgUrl { get; set; }
        public List<DiscountList>? Discounts { get; set; }

       

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
