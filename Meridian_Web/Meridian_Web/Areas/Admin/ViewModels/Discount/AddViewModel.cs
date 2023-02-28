namespace Meridian_Web.Areas.Admin.ViewModels.Discount
{
    public class AddViewModel
    {
        public string Title { get; set; }
        public int DiscontPers { get; set; }
        public DateTime DiscountTime { get; set; }
        public AddViewModel()
        {
                
        }

        public AddViewModel(string title, int discontPers, DateTime discountTime)
        {
            Title = title;
            DiscontPers = discontPers;
            DiscountTime = discountTime;
        }
    }
}
