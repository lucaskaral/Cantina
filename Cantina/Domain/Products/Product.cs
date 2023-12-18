using Flunt.Notifications;

namespace CantinaWebAPI.Domain.Products
{
    public class Product : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public required string Name { get; set;}
        public required double Price { get; set;}
        public string? Description { get; set;}

    }
}
