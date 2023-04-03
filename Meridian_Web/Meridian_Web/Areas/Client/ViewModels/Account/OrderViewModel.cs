using Meridian_Web.Areas.Client.ViewModels.OrderProducts;
using Meridian_Web.Database.Models.Enums;

namespace Meridian_Web.Areas.Client.ViewModels.Account
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public DateTime CreatAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
      
        public OrderViewModel(string id, DateTime creatAt, OrderStatus status, decimal total)
        {
            Id = id;
            CreatAt = creatAt;
            Status = status;
            Total = total;
           
        }
    }
}
