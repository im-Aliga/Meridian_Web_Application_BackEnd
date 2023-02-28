namespace BackEndFinalProject.Areas.Admin.ViewModels.Plant
{
    public class TagListItemViewModel
    {

        public int Id { get; set; }
        public string Tagname { get; set; }
        public TagListItemViewModel(int ıd, string tagname)
        {
            Id = ıd;
            Tagname = tagname;
        }


      
    }
}
