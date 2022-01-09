using FreakyFashionService.OrderProcessor.Model.Domain;
using FreakyFashionService.OrderProcessor.Model.Dto;

namespace FreakyFashionServices.OrderService.Model.Dto
{
    public class BasketDto
    {
        public string Identifier { get; set; }
        public List<Product> Items { get; set; }
    }
}
