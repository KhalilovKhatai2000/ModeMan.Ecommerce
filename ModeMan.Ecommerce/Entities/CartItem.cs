namespace ModeMan.Ecommerce.Entities
{
    public sealed class CartItem : BaseEntity
    {
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice => Count * (Product?.Price ?? 0);

    }
}
