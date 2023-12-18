using CantinaWebAPI.Domain.Orders;
using CantinaWebAPI.Domain.OrdersProducts;
using CantinaWebAPI.Domain.Products;
using CantinaWebAPI.Domain.Users;
using CantinaWebAPI.Infra.Data;
using Rabbit.Models.Entities;
using Rabbit.Services.Interfaces;

namespace CantinaWebAPI.EndPoints.Orders
{
    public class OrderPost
    {
        public static string Template => "/orders";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(OrderRequest orderRequest, ApplicationDbContext context, IRabbitMessageService rabbitMessageService)
        {
            var user = context.Users.Where(u => u.Id.Equals(orderRequest.UserId)).FirstOrDefault();
            if (user == null)
            {
                return Results.BadRequest("Usuário inválido!");
            }

            Order order = new Order { Id = Guid.NewGuid(), DateTime = orderRequest.DateTime, Status = 0, IdUser = user.Id };
            context.Orders.Add(order);
            foreach (var orderProduct in orderRequest.OrderProducts)
            {
                var product = context.Products.Where(p => p.Id.Equals(orderProduct.IdProduct)).FirstOrDefault();
                if (product == null)
                    return Results.BadRequest("Pedido inválido.");

                    if (order.OrderProducts == null)
                        order.OrderProducts = new List<OrderProducts>();

                orderProduct.UnitPrice = product.Price;
                orderProduct.IdOrder = order.Id;

                context.OrderProducts.Add(orderProduct);
            }
            
            context.SaveChanges();


            RabbitMessage rabbitMessage = new RabbitMessage { Id = order.Id, Title = user.Name, Text = order.ToString() };
            rabbitMessageService.SendMessage(rabbitMessage);

            return Results.Created($"/order/{order.Id}", order.Id);
        }

    }
}
