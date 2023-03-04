namespace Meridian_Web.Areas.Admin.ViewModels.Product
{
    public class ColorListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ColorListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
