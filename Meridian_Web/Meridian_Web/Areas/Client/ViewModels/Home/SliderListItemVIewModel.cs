namespace Meridian_Web.Areas.Client.ViewModels.Home
{
    public class SliderListItemVIewModel
    {

        public string Title { get; set; }
        public string OfferContext { get; set; }
        public string Content { get; set; }
        public string StartPrice { get; set; }
        public string ButtonName { get; set; }
        public string BtnRedirectUrl { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public SliderListItemVIewModel(string title, string offerContext, string content, string startPrice, string buttonName, string btnRedirectUrl, int order, string ımageUrl)
        {
            Title = title;
            OfferContext = offerContext;
            Content = content;
            StartPrice = startPrice;
            ButtonName = buttonName;
            BtnRedirectUrl = btnRedirectUrl;
            Order = order;
            ImageUrl = ımageUrl;
        }
    }
}
