namespace Meridian_Web.Areas.Client.ViewModels.Home
{
    public class PaymentBenefitsViewModel
    {

        public string Title { get; set; }
        public string Context { get; set; }
        public string ImageUrl { get; set; }
        public PaymentBenefitsViewModel(string title, string context, string ımageUrl)
        {
            Title = title;
            Context = context;
            ImageUrl = ımageUrl;
        }
    }
}
