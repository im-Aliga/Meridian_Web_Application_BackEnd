using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class Contact : BaseEntity<int>, IAuditable
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }   
        public string Message { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
