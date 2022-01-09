using FreakyFashionServices.OrderService.Model.Domain;

namespace FreakyFashionServices.OrderService.Model.Dto
{
    public class BasketDto
    {
        public string identifier { get; set; }
        public List<OrderLine> items { get; set; }
    }
}
