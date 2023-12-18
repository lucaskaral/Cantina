
namespace CantinaWebAPI.Domain.OrdersProducts
{
    public class OrderProducts
    {
        public Guid Id { get; set; }
        public Guid IdOrder { get; set; }
        public Guid IdProduct { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get; set; }

    }
}
