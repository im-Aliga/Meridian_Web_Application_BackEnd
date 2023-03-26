
namespace BackEndFinalProject.Areas.Client.ViewModels.Home.Modal
{
    public class ModalViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int InStock { get; set; }
        public string ImgUrl { get; set; }
        public List<DiscountList>? Discounts { get; set; }
        public int? ColorId { get; set; }
        public List<ColorViewModeL>? Colors { get; set; }
        public int? SizeId { get; set; }
        public List<SizeViewModeL>? Sizes { get; set; }
        public ModalViewModel()
        {
            
        }
        public ModalViewModel(int ıd, string title, string description, decimal price, decimal? discountPrice, int ınStock, string ımgUrl, List<DiscountList>? discounts, List<ColorViewModeL>? colors, List<SizeViewModeL>? sizes)
        {
            Id = ıd;
            Title = title;
            Description = description;
            Price = price;
            DiscountPrice = discountPrice;
            InStock = ınStock;
            ImgUrl = ımgUrl;
            Discounts = discounts;
            Colors = colors;
            Sizes = sizes;
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

    }

}
