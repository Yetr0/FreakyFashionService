namespace FreakyFashionService.BasketService.Model.Dto
{
    public class BasketDto
    {
        public string Identifier { get; set; }
        public List<ProductDto> Items { get; set; }
    }
}
