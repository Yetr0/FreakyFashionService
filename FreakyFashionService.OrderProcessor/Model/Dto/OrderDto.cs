using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFashionService.OrderProcessor.Model.Dto
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string Identifier { get; set; }
        public string Customer { get; set; }
    }
}
