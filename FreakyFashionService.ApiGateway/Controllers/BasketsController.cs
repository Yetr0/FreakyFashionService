using FreakyFashionService.ApiGateway.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashionService.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public BasketsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPut("{identifier}")]
        public async Task<IActionResult> CreateBasket(BasketDto basketDto)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var httpResponeMessage = await httpClient.PutAsJsonAsync($"http://localhost:5000/api/baskets/{basketDto.Identifier}", basketDto);

            if (httpResponeMessage.IsSuccessStatusCode)
                return NoContent();

            return BadRequest(httpResponeMessage);
        }

        [HttpGet("{identifier}")]
        public async Task<IActionResult> GetBasket(string identifier)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var httpResponeMessage = await httpClient.GetFromJsonAsync<BasketDto>($"http://localhost:5000/api/baskets/{identifier}");

            return Ok(httpResponeMessage);
        }
    }
}
