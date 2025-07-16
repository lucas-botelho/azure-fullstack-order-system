namespace OrdersApi.Models
{
    public class Order
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
    }
}
