namespace FreakyFashionService.ApiGateway.Model.Dto
{
    public class BasketDto
    {
        public string Identifier { get; set; }
        public List<BasketProduct> Items { get; set; }
    }
}
