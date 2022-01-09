using FreakyFashionServices.OrderService.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashionServices.OrderService.Data
{
    public class OrderServiceContext : DbContext
    {
        public OrderServiceContext(DbContextOptions<OrderServiceContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Order { get; set; }
    }
}
