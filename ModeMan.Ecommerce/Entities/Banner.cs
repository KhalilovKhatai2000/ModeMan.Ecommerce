namespace ModeMan.Ecommerce.Entities
{
    public sealed class Banner : BaseEntity
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public Guid? ImageId { get; set; }
        public Image? Image { get; set; }

    }
}
