using FreakyFashionService.ApiGateway.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FreakyFashionService.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponse = await httpClient.GetAsync("http://localhost:5002/api/products");

            if (httpResponse.IsSuccessStatusCode)
            {
                var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                var productList = JsonSerializer.Deserialize
                    <IEnumerable<ProductDto>>(contentStream);



                var stockHttpResponse = await httpClient.GetAsync("http://localhost:5001/api/stock");

                var stockContentStream = await stockHttpResponse.Content.ReadAsStreamAsync();

                var stockLevels = JsonSerializer.Deserialize<IEnumerable<StockDto>>(stockContentStream);

                if (productList != null && stockLevels != null)
                {
                    foreach (var product in productList)
                    {
                        var stockLevel = stockLevels.FirstOrDefault(x => x.articleNumber.ToLower() == product.articleNumber.ToLower());
                        if (stockLevel != null)
                            product.stockLevel = stockLevel.stock;
                        else
                        {
                            product.stockLevel = 0;
                        }
                    }


                    return Ok(productList);
                }
            }

            return BadRequest("Unable to get products and or stocklevel");
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(PostProductDto product)
        {
            var httpClient = _httpClientFactory.CreateClient();

            using var httpResponeMessage = await httpClient.PostAsJsonAsync("http://localhost:5002/api/products", product);

            var content = await httpResponeMessage.Content.ReadAsStringAsync();

            return Created("",content);
        }
    }
}
