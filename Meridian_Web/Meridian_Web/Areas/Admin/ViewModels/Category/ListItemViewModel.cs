namespace Meridian_Web.Areas.Admin.ViewModels.Category
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListItemViewModel(int ıd, string title, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Title = title;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }
}
