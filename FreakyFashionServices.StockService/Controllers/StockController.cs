using FreakyFashionServices.StockService.Data;
using FreakyFashionServices.StockService.Models.Domain;
using FreakyFashionServices.StockService.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashionServices.StockService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        public StockController(StockServiceContext context)
        {
            Context = context;
        }

        public StockServiceContext Context { get; set; }

        [HttpPut("{articleNumber}")]
        public IActionResult UpdateStockLevel(string articleNumber, UpdateStockLevelDto updateStockLevelDto)
        {

            var stockLevel = Context.StockLevel
                .FirstOrDefault(x => x.ArticleNumber == updateStockLevelDto.ArticleNumber);

            if(stockLevel == null)
            {
                var stocklevel = new StockLevel(
                   updateStockLevelDto.ArticleNumber,
                   updateStockLevelDto.StockLevel
                   );

                Context.StockLevel.Add(stocklevel);
            }
            else
            {
                stockLevel.Stock = updateStockLevelDto.StockLevel;
            }
            
            Context.SaveChangesAsync();

            return Created("", null);
        }

        [HttpGet]
        public IEnumerable<StockLevelDto> GetAll()
        {
            var stockLevelDtos = Context.StockLevel.Select(x => new StockLevelDto
            {
                ArticleNumber = x.ArticleNumber,
                Stock = x.Stock
            });

            return stockLevelDtos;
        }

    }
}
