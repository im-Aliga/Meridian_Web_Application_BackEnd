namespace Meridian_Web.Areas.Client.ViewModels.OrderProducts
{
    public class OrderProductListItemViewModel
    {
        public OrderProductListItemViewModel(int id, string imgURl, string productName,  int quantity, int total)
        {
            Id = id;
            ImgURl = imgURl;
            ProductName = productName;
            Quantity = quantity;
            Total = total;
        }

        public int Id { get; set; }
        public string ImgURl { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
