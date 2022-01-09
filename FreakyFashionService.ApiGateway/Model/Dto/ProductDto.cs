namespace FreakyFashionService.ApiGateway.Model.Dto
{
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string price { get; set; }
        public string articleNumber { get; set; }
        public string urlSlug { get; set; }
        public int stockLevel { get; set; }
    }
}
