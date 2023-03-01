namespace Meridian_Web.Areas.Admin.ViewModels.Size
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListItemViewModel(int ıd, string name, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }
}
