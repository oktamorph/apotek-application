namespace Order.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Guid OrderGuid { get; set; }
        public string Description { get; set; }
        public required decimal Price { get; set; }
    }
}
