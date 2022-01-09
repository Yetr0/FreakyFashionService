using FreakyFashionService.OrderProcessor.Data;
using FreakyFashionService.OrderProcessor.Model.Domain;
using FreakyFashionService.OrderProcessor.Model.Dto;
using FreakyFashionService.OrderProcessor.Services;
using FreakyFashionServices.OrderService.Model.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Json;
using System.Text;


var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddTransient<MyHttpClient>();
    })
    .Build();

var context = new OrderProcessorContext();

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
   queue: "order",
   durable: true,
   exclusive: false,
   autoDelete: false,
   arguments: null);

var consumer = new EventingBasicConsumer(channel);

var orderManager = new OrderManager(context);

consumer.Received += (sender, e) => {

    Console.WriteLine("Processing Order...");

    var body = e.Body.ToArray();
    var json = Encoding.UTF8.GetString(body);

    var orderDto = JsonConvert.DeserializeObject<OrderDto>(json);

    var httpClient = host.Services.GetRequiredService<MyHttpClient>();

    var basketDto = httpClient.GetProducts(orderDto?.Identifier);

    if (basketDto.Result.Count() > 0)
    {
        var order = new Order
        {
            Customer = orderDto.Customer,
            OrderIdentifier = orderDto.OrderId,
            Products = basketDto?.Result.ToList()
        };

        orderManager.Register(order);
    }
};

channel.BasicConsume(
   queue: "order",
   autoAck: true,
   consumer: consumer);

Console.WriteLine(" Press [ENTER] to exit.");
Console.ReadLine();

public class MyHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public MyHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Product>> GetProducts(string identifier)
    {

        var httpClient = _httpClientFactory.CreateClient();

        var httpResponeMessage = await httpClient.GetFromJsonAsync<BasketDto>($"http://localhost:5000/api/baskets/{identifier}");

        if(httpResponeMessage != null)
        return httpResponeMessage.Items;

        return null;
    }
}
