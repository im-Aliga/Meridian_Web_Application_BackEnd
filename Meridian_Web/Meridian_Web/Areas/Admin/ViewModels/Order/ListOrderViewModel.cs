using Meridian_Web.Database.Models.Enums;

namespace Meridian_Web.Areas.Admin.ViewModels.Order
{
    public class ListOrderViewModel
    {

        public string Id { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListOrderViewModel(string id, OrderStatus status, decimal total, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Status = status;
            Total = total;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
