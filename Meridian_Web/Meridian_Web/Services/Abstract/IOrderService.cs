namespace Meridian_Web.Services.Abstracts
{
    public interface IOrderService
    {
        Task<string> GenerateUniqueTrackingCodeAsync();
    }
}
