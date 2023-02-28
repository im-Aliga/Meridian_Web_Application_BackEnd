namespace BackEndFinalProject.Areas.Admin.ViewModels.Plant
{
    public class CatagoryListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public CatagoryListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
