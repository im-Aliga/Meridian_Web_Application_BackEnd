namespace BackEndFinalProject.Areas.Admin.ViewModels.Plant
{
    public class SizeListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public SizeListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
