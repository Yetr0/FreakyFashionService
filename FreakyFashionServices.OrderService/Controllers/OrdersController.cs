using FreakyFashionServices.OrderService.Data;
using FreakyFashionServices.OrderService.Model.Domain;
using FreakyFashionServices.OrderService.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FreakyFashionServices.OrderService.Controllers
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
        public IActionResult CreateOrder(OrderDto orderDto)
        {
            var guid = Guid.NewGuid();

            var order = new Order()
            {
                OrderId = guid,
                Customer = orderDto.Customer,
                Identifier = orderDto.Identifier
            };

            var returndto = new
            {
                OrderId = guid,
            };

            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            
            channel.ConfirmSelect();

            channel.QueueDeclare(
                queue: "order",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order));


            channel.BasicPublish(
                exchange: "",
                routingKey: "order",
                basicProperties: null,
                body: body);

            return Created("", returndto);
        }
    }
}
