namespace Meridian_Web.Areas.Admin.ViewModels.Plant
{
    public class SizeListItemViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public SizeListItemViewModel(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }

       
    }
}
