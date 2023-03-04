namespace Meridian_Web.Areas.Admin.ViewModels.Product
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
