using ModeMan.Ecommerce.Enums;

namespace ModeMan.Ecommerce.Entities
{
    public sealed class Image : BaseEntity
    {
        public string Url { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public ImageType? ImageType { get; set; }
    }
}
