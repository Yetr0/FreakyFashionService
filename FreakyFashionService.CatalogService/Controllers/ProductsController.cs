using FreakyFashionService.CatalogService.Data;
using FreakyFashionService.CatalogService.Models.Domain;
using FreakyFashionService.CatalogService.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FreakyFashionService.CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public CatalogServiceContext Context { get; set; }
        public ProductsController(CatalogServiceContext conetext)
        {
            Context = conetext;
        }

        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            var products = Context.Product.Select(x => new ProductDto(
                x.Id,
                x.Name,
                x.Description,
                x.ImageUrl,
                x.Price,
                x.ArticleNumber,
                x.UrlSlug
                ));

            return products;
        }

        [HttpPost]
        public IActionResult CreateProduct(PostProductDto postProductDto)
        {
            var product = new Product(
                postProductDto.Name,
                postProductDto.Description,
                postProductDto.ImageUrl,
                postProductDto.Price,
                postProductDto.ArticleNumber,
                CreateUrlSlug(postProductDto.Name));

            Context.Product.Add(product);

            Context.SaveChanges();

            //Gör snabbt om från Product till ProductDto som kan returneras i responsen.
            var createdProduct = JsonConvert.DeserializeObject<ProductDto>(JsonConvert.SerializeObject(product));

            return Created("", createdProduct);
        }

        private string CreateUrlSlug(string productName) =>
            productName.Replace("-", "").Replace(' ', '-').ToLower();
    }
}
