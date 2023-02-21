namespace Meridian_Web.Database.Models.Common
{
    public abstract class BaseNavbarAndSubNavbar : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int RowNumber { get; set; }
    }
}
