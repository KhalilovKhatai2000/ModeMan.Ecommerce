namespace ModeMan.Ecommerce.Entities
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid? ImageId { get; set; }
        public Image? Image { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
