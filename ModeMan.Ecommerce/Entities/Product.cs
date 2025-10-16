namespace ModeMan.Ecommerce.Entities
{
    public sealed class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Image>? Images { get; set; } = new List<Image>();
    }
}
