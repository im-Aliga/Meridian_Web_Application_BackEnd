using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class BlogCategory : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
       
        public List<BlogAndBlogCategory> BlogCatagories { get; set; }
       
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
