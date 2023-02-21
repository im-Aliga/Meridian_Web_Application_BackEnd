using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Blog : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Proverb { get; set; }
        public string ProverbAuthor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BlogFile>? BlogFile { get; set; }
        public List<Comment> Comments { get; set; }
        public List<BlogAndBlogTag>? BlogTags { get; set; }
        public List<BlogAndBlogCategory>? BlogCategory { get; set; }





    }
}
