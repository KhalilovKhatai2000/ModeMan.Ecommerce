using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Enums;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Services.Concreete
{
    public class ImageService<T> : IImageService<T> where T : BaseEntity
    {
        private readonly ModeManDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ImageService(ModeManDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<Image> CreateImageAsync(IFormFile file, ImageType type, T entity)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Şəkil seçilməyib.");

            // Fayllar üçün fiziki path
            string uploadPath = Path.Combine(_env.WebRootPath, "ModeMan", "img");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Fayl adı
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(uploadPath, fileName);

            // Faylı yazmaq
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Yalnız nisbətən URL saxla (wwwrootdan sonrakı)
            string relativeUrl = Path.Combine("ModeMan", "img", fileName).Replace("\\", "/");

            var image = new Image
            {
                Id = Guid.NewGuid(),
                Url = "/" + relativeUrl, // vebdə istifadə üçün
                ImageType = type
            };

            switch (entity)
            {
                case Product p:
                    image.ProductId = p.Id;
                    break;
            }

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<List<Image>> CreateBulkImageAsync(List<IFormFile> files, ImageType imageType, T entity)
        {
            var images = new List<Image>();
            foreach (var file in files)
            {
                var image = await CreateImageAsync(file, imageType, entity);
                images.Add(image);
            }
            return images;
        }

        public void DeleteImage(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            // Əgər path "/" ilə başlayırsa, təmizləyirik
            var relativePath = filePath.TrimStart('/');
            var fullPath = Path.Combine(_env.WebRootPath, relativePath);

            // DB-də tapırıq
            var image = _context.Images
                .FirstOrDefault(x => x.Url == filePath || x.Url == "/" + relativePath);
            if (image != null)
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
            }

            // Fiziki faylı silirik
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public void DeleteBulkImage(List<string> filePaths)
        {
            foreach (var path in filePaths)
            {
                DeleteImage(path);
            }
        }

        public async Task<List<Image>> GetAllAsync()
        {
            return await _context.Images.ToListAsync();
        }
    }
}
