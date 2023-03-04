namespace Meridian_Web.Areas.Admin.ViewModels.ProductImage
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public List<ListItem>? Images { get; set; }

        public class ListItem
        {
            public int Id { get; set; }
            public string? ImageUrl { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
