namespace ModeMan.Ecommerce.Entities
{
    public sealed class Slider : BaseEntity
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public Guid? ImageId { get; set; }
        public Image? Image { get; set; }
    }
}
