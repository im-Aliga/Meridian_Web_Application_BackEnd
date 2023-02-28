namespace Meridian_Web.Areas.Admin.ViewModels.Tag
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ListItemViewModel(int ıd, string tagName, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            TagName = tagName;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
