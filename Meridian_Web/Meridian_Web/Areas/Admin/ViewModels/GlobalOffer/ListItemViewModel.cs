namespace Meridian_Web.Areas.Admin.ViewModels.GlobalOffer
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string MainContext { get; set; }
        public string Context { get; set; }
        public string ButtonContext { get; set; }
        public DateTime OfferTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListItemViewModel(int ıd, string title, string mainContext, string context, string buttonContext, DateTime offerTime, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Title = title;
            MainContext = mainContext;
            Context = context;
            ButtonContext = buttonContext;
            OfferTime = offerTime;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }
}
