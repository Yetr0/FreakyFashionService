namespace FreakyFashionServices.OrderService.Model.Domain
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string Identifier { get; set; }
        public string Customer { get; set; }
    }
}
