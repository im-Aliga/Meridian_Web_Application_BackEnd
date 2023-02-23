namespace Meridian_Web.Areas.Admin.ViewModels.Banner
{
    public class AddBannerViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string MainContext { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
