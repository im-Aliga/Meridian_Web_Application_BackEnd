
using System.ComponentModel.DataAnnotations;

namespace Meridian_Web.Areas.Admin.ViewModels.Product
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int InStock { get; set; }
        public string Content { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> BrandsIds { get; set; }
        public List<int>? DiscountIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> SizeIds { get; set; }
        public List<CatagoryListItemViewModel>? Categories { get; set; }
        public List<DiscountListViewModel>? Discounts { get; set; }
        public List<BrandListItemViewModel>? Brands { get; set; }
        public List<SizeListItemViewModel>? Sizes { get; set; }
        public List<ColorListItemViewModel>? Colors { get; set; }
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
