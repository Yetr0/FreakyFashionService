namespace FreakyFashionService.CatalogService.Models.Dto
{
    public class ProductDto
    {
        public ProductDto(int id, string name, string description, string imageUrl, string price, string articleNumber, string urlSlug)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            ArticleNumber = articleNumber;
            UrlSlug = urlSlug;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public string ArticleNumber { get; set; }
        public string UrlSlug { get; set; }
    }
}
