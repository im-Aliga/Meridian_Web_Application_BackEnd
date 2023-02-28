namespace Meridian_Web.Areas.Admin.ViewModels.Discount
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int DiscontPers { get; set; }
        public DateTime DiscountTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListItemViewModel(int ıd, string title, int discontPers, DateTime discountTime, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Title = title;
            DiscontPers = discontPers;
            DiscountTime = discountTime;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
    }
}
