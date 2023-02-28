using System.ComponentModel.DataAnnotations;

namespace BackEndFinalProject.Areas.Admin.ViewModels.Plant
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Rate { get; set; }
        [Required]
        public List<int>? CategoryIds { get; set; }
        [Required]
        public List<int>? ColorIds { get; set; }

        [Required]
        public List<int>? TagIds { get; set; }
        [Required]
        public List<int>? SizeIds { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
       
        public List<CatagoryListItemViewModel>? Categories { get; set; }
        
        public List<SizeListItemViewModel>? Sizes { get; set; }
 
        public List<ColorListItemViewModel>? Colors { get; set; }
     
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
