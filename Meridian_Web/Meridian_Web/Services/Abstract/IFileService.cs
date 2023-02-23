

using Meridian_Web.Contracts.File;

namespace Meridian_Web.Services.Abstracts
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory);
        string GetFileUrl(string? fileName, UploadDirectory uploadDirectory);
        Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory);
    }
}
