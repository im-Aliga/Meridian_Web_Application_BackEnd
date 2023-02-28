
using System.ComponentModel.DataAnnotations;

namespace BackEndFinalProject.Areas.Admin.ViewModels.Plant
{
    public class AddViewModel
    {
    
        public string Name { get; set; }
        public int Price { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> SizeIds { get; set; }
        public string Description { get; set; }
        public List<CatagoryListItemViewModel>? Categories { get; set; }
        public List<SizeListItemViewModel>? Sizes { get; set; }
        public List<ColorListItemViewModel>? Colors { get; set; }
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
