namespace BackEndFinalProject.Areas.Admin.ViewModels.About
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListItemViewModel(int ıd, string title, string content, DateTime updatedAt)
        {
            Id = ıd;
            Title = title;
            Content = content;
            UpdatedAt = updatedAt;
        }
       
       
    }
}
