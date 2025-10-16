namespace ModeMan.Ecommerce.Entities
{
    public sealed class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser? AppUser { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}