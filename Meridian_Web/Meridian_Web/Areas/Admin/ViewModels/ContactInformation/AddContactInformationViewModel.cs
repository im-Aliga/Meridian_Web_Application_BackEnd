namespace Meridian_Web.Areas.Admin.ViewModels.ContactInformation
{
    public class AddContactInformationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainContext { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
