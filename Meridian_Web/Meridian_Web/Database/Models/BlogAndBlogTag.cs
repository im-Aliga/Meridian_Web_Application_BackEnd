using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class BlogAndBlogTag : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog? Blog{ get; set; }

        public int BlogTagId { get; set; }
        public BlogTag Tag { get; set; }
    }
}
