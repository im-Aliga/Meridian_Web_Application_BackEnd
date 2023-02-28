namespace Meridian_Web.Areas.Admin.ViewModels.Brand
{
    public class ListBrandViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListBrandViewModel(int ıd, string name, string ımageUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Name = name;
            ImageUrl = ımageUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }
}
