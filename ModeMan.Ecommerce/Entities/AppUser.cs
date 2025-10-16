using Microsoft.AspNetCore.Identity;

namespace ModeMan.Ecommerce.Entities
{
    public sealed class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public Cart? Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
