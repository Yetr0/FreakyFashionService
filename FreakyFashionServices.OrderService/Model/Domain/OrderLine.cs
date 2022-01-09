using System.ComponentModel.DataAnnotations;

namespace FreakyFashionServices.OrderService.Model.Domain
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int productId{ get; set; }
        public int quantity { get; set; }
    }
}
