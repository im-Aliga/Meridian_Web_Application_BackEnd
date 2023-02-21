using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Comment: BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public string Email { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
