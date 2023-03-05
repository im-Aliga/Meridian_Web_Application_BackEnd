namespace Meridian_Web.Areas.Admin.ViewModels.FeedBack
{
    public class ListFeedBackViewModel
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Context { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListFeedBackViewModel(int id, string fullName, string context, string role, string imageUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            FullName = fullName;
            Context = context;
            Role = role;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }



    }
}
