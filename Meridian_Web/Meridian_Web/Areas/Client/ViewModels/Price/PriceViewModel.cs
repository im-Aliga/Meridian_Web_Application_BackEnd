namespace Meridian_Web.Areas.Client.ViewModels.Price
{
    public class PriceViewModel
    {
        public PriceViewModel(int? startPrice, int? endPrice)
        {
            StartPrice = startPrice;
            EndPrice = endPrice;
        }
        public PriceViewModel()
        {
            
        }

        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; } 
    }
}
