using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Enums;

namespace ModeMan.Ecommerce.Services.Abstract
{
    public interface IImageService<T> where T : BaseEntity
    {
        Task<Image> CreateImageAsync(IFormFile file, ImageType imageType, T entity);
        Task<List<Image>> CreateBulkImageAsync(List<IFormFile> files, ImageType imageType, T entity);
        void DeleteImage(string filePath);
        void DeleteBulkImage(List<string> filePaths);
        Task<List<Image>> GetAllAsync();
    }
}
