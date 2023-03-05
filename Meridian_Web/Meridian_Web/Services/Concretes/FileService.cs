using Meridian_Web.Contracts.File;
using Meridian_Web.Services.Abstracts;

namespace Meridian_Web.Services.Concretes
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService>? _logger;

        public FileService(ILogger<FileService>? logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory)
        {
            string directoryPath = GetUploadDirectory(uploadDirectory);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imageNameInSystem = GenerateUniqueFileName(formFile.FileName);
            var filePath = Path.Combine(directoryPath, imageNameInSystem);

            try
            {
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch (Exception e)
            {
                _logger!.LogError(e, "Error occured in file service");

                throw;
            }

            return imageNameInSystem;
        }

        public async Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory)
        {
            var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory), fileName);

            await Task.Run(() => File.Delete(deletePath));
        }

        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            string startPath = Path.Combine("wwwroot", "client", "custom-files");

            switch (uploadDirectory)
            {
                case UploadDirectory.Product:
                    return Path.Combine(startPath, "products");
                case UploadDirectory.Slider:
                    return Path.Combine(startPath, "sliders");
                case UploadDirectory.Banner:
                    return Path.Combine(startPath, "banners");
                case UploadDirectory.Payment:
                    return Path.Combine(startPath, "payments");
                case UploadDirectory.FeedBack:
                    return Path.Combine(startPath, "feedbacks");
                case UploadDirectory.Blog:
                    return Path.Combine(startPath, "blogs");
                case UploadDirectory.Brand:
                    return Path.Combine(startPath, "brands");
                case UploadDirectory.ContactInformation:
                    return Path.Combine(startPath, "contactinformations");
                default:
                    throw new Exception("Something went wrong");
            }
        }
            
        private string GenerateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory)
        {
            string initialSegment = "client/custom-files/";

            switch (uploadDirectory)
            {
                case UploadDirectory.Product:
                    return $"{initialSegment}/products/{fileName}";
                case UploadDirectory.Slider:
                    return $"{initialSegment}/sliders/{fileName}";
                case UploadDirectory.Banner:
                    return $"{initialSegment}/banners/{fileName}";
                case UploadDirectory.Payment:
                    return $"{initialSegment}/payments/{fileName}";
                case UploadDirectory.FeedBack:
                    return $"{initialSegment}/feedbacks/{fileName}";
                case UploadDirectory.Blog:
                    return $"{initialSegment}/blogs/{fileName}";
                case UploadDirectory.Brand:
                    return $"{initialSegment}/brands/{fileName}";
                case UploadDirectory.ContactInformation:
                    return $"{initialSegment}/contactinformations/{fileName}";
                default:
                    throw new Exception("Something went wrong");
            }
        }
    }
}
