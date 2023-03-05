namespace Meridian_Web.Areas.Admin.ViewModels.FeedBack
{
    public class AddFeedbackViewModel
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string Context { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
