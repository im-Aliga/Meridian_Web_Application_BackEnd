


namespace Meridian_Web.Areas.Admin.ViewModels.Blog
{
    public class AddViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Proverb { get; set; }
        public string ProverbAuthor { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> TagIds { get; set; }
        
        public List<CatagoryListItemViewModel>? Categories { get; set; }
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
