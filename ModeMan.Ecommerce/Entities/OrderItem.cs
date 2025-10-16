namespace ModeMan.Ecommerce.Entities
{
    public sealed class OrderItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
