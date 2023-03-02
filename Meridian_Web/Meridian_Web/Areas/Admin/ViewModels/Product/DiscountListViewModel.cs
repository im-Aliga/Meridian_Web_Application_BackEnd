namespace Meridian_Web.Areas.Admin.ViewModels.Product
{
    public class DiscountListViewModel
    {

        public int Id { get; set; }    
        public string Title { get; set; }
        public int DiscontPers { get; set; }
        public DateTime DiscountTime { get; set; }
        public DiscountListViewModel(int ıd, string title, int discontPers, DateTime discountTime)
        {
            Id = ıd;
            Title = title;
            DiscontPers = discontPers;
            DiscountTime = discountTime;
        }

        public DiscountListViewModel(int ıd, string title)
        {
            Id = ıd;
            Title = title;
        }

        public DiscountListViewModel(int discontPers)
        {
            DiscontPers = discontPers;
        }
    }
}
