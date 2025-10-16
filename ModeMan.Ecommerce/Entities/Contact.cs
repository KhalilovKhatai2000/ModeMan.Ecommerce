namespace ModeMan.Ecommerce.Entities
{
    public sealed class Contact : BaseEntity
    {
        public string Header { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CountryName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
