using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class BlogAndBlogCategory : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory Category { get; set; }

       
    }
}
