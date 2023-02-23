namespace Meridian_Web.Areas.Admin.ViewModels.Slider
{
    public class ListSliderViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string OfferContext { get; set; }
        public string Content { get; set; }
        public string StartPrice { get; set; }
        public string ButtonName { get; set; }
        public string BtnRedirectUrl { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListSliderViewModel(int ıd, string title, string offerContext, string content, string startPrice, string buttonName, string btnRedirectUrl, int order, string ımageUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Title = title;
            OfferContext = offerContext;
            Content = content;
            StartPrice = startPrice;
            ButtonName = buttonName;
            BtnRedirectUrl = btnRedirectUrl;
            Order = order;
            ImageUrl = ımageUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        
    }
}

