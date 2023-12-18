using System.Xml.Linq;

namespace CantinaWebAPI.EndPoints.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public double TotalPrice { get; set; }
    }
}
