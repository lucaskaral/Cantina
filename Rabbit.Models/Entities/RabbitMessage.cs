using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Models.Entities
{
    public class RabbitMessage
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string OrderData { get; set; }
    }
}
