
using Meridian_Web.Database.Models;
using Meridian_Web.Database.Models.Common;

namespace BackEndFinalProject.Database.Models
{
    public class ProductImage : BaseEntity<int>, IAuditable
    {
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; } 



        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
