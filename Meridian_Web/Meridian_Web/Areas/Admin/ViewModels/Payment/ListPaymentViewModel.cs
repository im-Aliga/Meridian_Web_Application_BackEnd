namespace Meridian_Web.Areas.Admin.ViewModels.Payment
{
    public class ListPaymentViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListPaymentViewModel(int ıd, string title, string context, string ımageUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Title = title;
            Context = context;
            ImageUrl = ımageUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
