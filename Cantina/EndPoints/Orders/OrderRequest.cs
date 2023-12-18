using CantinaWebAPI.Domain.Orders;
using CantinaWebAPI.Domain.OrdersProducts;
using CantinaWebAPI.Domain.Products;

namespace CantinaWebAPI.EndPoints.Orders
{
    public class OrderRequest
    {
        public DateTime DateTime { get; set; }
        public Guid UserId { get; set; }
        public List<OrderProducts>  OrderProducts { get; set; }
    }
}
