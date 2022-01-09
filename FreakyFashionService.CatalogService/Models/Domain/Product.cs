namespace FreakyFashionService.CatalogService.Models.Domain
{
    public class Product
    {
        public Product(string name, string description, string imageUrl, string price, string articleNumber, string urlSlug)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            ArticleNumber = articleNumber;
            UrlSlug = urlSlug;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public string Price { get; init; }
        public string ArticleNumber { get; init; }
        public string UrlSlug { get; init; }
    }
}
