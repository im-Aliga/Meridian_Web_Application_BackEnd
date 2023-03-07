namespace Meridian_Web.Areas.Admin.ViewModels.TeamMember
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
