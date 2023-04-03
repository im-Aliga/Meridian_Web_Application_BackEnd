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

        public int? StartPrice { get; set; }
        public int? EndPrice { get; set; } 
    }
}
