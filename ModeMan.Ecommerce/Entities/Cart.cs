namespace ModeMan.Ecommerce.Entities
{
    public sealed class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
