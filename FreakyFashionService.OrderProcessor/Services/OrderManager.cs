using FreakyFashionService.OrderProcessor.Data;
using FreakyFashionService.OrderProcessor.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFashionService.OrderProcessor.Services
{
    public class OrderManager
    {
        public OrderManager(OrderProcessorContext context)
        {
            Context = context;
        }
        private OrderProcessorContext Context { get; }

        public void Register(Order order)
        {
            Context.Order.Add(order);

            Context.SaveChanges();
        }
    }
}
