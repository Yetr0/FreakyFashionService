using FreakyFashionService.BasketService.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace FreakyFashionService.BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        public IDistributedCache Cache { get; }

        public BasketsController(IDistributedCache cache)
        {
            Cache = cache;
        }

        [HttpGet("{identifier}")]
        public async Task<IActionResult> GetBasket(string identifier)
        {
            var serializedbasket = await Cache.GetStringAsync(identifier);

            if(serializedbasket == null)
                return NotFound();  

            var basketDto = JsonSerializer.Deserialize<BasketDto>(serializedbasket);

            return Ok(basketDto);
        }

        [HttpPut("{identifier}")]
        public IActionResult CreateBasket(string identifier, BasketDto basketDto)
        {
            if(basketDto.Identifier != identifier)
                return BadRequest("The body identifier and identifier are not the same");

            var serializedRegistration = JsonSerializer.Serialize(basketDto);

            Cache.SetString(basketDto.Identifier, serializedRegistration);

            return Created("", basketDto);
        }
    }
}
