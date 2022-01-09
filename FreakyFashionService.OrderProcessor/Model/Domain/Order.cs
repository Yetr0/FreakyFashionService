using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFashionService.OrderProcessor.Model.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Guid OrderIdentifier { get; set; }
        public string Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}
