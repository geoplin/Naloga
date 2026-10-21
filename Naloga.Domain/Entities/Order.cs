namespace Naloga.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
