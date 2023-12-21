using CantinaWebAPI.Domain.OrdersProducts;
using Flunt.Notifications;

namespace CantinaWebAPI.Domain.Orders
{
    public class Order : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set;}
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public List<OrderProducts> OrderProducts { get; set; }
        public double OrderPrice { get; set; }
    }
}
