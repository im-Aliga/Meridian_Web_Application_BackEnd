namespace Meridian_Web.Areas.Admin.ViewModels.TeamMember
{
    public class ListViewModel
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImageUrl { get; set; }
        public ListViewModel(int ıd, string fullName, string position, DateTime createdAt, DateTime updatedAt, string ımageUrl)
        {
            Id = ıd;
            FullName = fullName;
            Position = position;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ImageUrl = ımageUrl;
        }
    }
}
