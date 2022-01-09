using FreakyFashionService.ApiGateway.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashionService.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OrdersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.PostAsJsonAsync("http://localhost:5003/api/orders", orderDto);

            return Created("", httpResponseMessage);
        }
    }
}
