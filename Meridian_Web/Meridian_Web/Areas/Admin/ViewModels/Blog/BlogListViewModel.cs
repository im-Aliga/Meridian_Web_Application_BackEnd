

namespace Meridian_Web.Areas.Admin.ViewModels.Blog
{
    public class BlogListViewModel
    {
        public BlogListViewModel(
            int ıd,
            string title, 
            string description,
            string proverb, 
            string proverbAuthor,
            DateTime createdAt,
            DateTime updatedAt,
            List<CategoryViewModeL> categories,
            List<TagViewModel> tags)
        {
            Id = ıd;
            Title = title;
            Description = description;
            Proverb = proverb;
            ProverbAuthor = proverbAuthor;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Categories = categories;
            Tags = tags;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Proverb { get; set; }
        public string ProverbAuthor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public List<CategoryViewModeL> Categories { get; set; }
        public List<TagViewModel> Tags { get; set; }
       

        



        public class CategoryViewModeL
        {

            public string Title { get; set; }
            public CategoryViewModeL(string title)
            {
                Title = title;
               
            }
            


        }
        public class TagViewModel
        {

            public string TagName { get; set; }
            public TagViewModel(string tagName)
            {
                TagName = tagName;
            }
        }




    }
}
