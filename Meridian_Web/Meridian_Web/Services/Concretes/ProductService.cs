using Meridian_Web.Services.Abstract;

namespace Meridian_Web.Services.Concretes
{
    public class ProductService : IProductService
    {
        public decimal FindPercent(decimal price, decimal? discountPrice, int disPercent)
        {
            decimal sumOfPers = (price * disPercent) / 100;
            decimal result = discountPrice.HasValue ? discountPrice.Value : price;
            result -= sumOfPers;
            return result;
        }
    }
}
