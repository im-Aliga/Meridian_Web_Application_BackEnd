using Meridian_Web.Database;
using Meridian_Web.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Services.Concretes
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private const string ORDER_TRACKING_CODE_PREFIX = "AA";
        private const int ORDER_TRACKING_MIN_RANGE = 10_000;
        private const int ORDER_TRACKING_MAX_RANGE = 100_000;

        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> GenerateUniqueTrackingCodeAsync()
        {
            string token = string.Empty;

            do
            {
                token = GenerateRandomTrackingCode();

            } while (await _dataContext.Orders.AnyAsync(o => o.Id == token));

            return token;
        }

        private string GenerateRandomTrackingCode()
        {
            return $"{ORDER_TRACKING_CODE_PREFIX}{Random.Shared.Next(ORDER_TRACKING_MIN_RANGE, ORDER_TRACKING_MAX_RANGE)}";
        }
    }
}
