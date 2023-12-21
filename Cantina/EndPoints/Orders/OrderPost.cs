using CantinaWebAPI.Domain.Orders;
using CantinaWebAPI.Domain.OrdersProducts;
using CantinaWebAPI.Domain.Products;
using CantinaWebAPI.Domain.Users;
using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Rabbit.Models.Entities;
using Rabbit.Services.Interfaces;

namespace CantinaWebAPI.EndPoints.Orders
{
    public class OrderPost
    {
        public static string Template => "/orders";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(OrderRequest orderRequest, ApplicationDbContext context, IRabbitMessageService rabbitMessageService, UserManager<IdentityUser> userManager)
        {
            var user = userManager.Users.Where(u => u.Id.Equals(orderRequest.UserId.ToString())).FirstOrDefault();
            if (user == null)
            {
                return Results.BadRequest("Usuário inválido!");
            }

            Order order = new Order { Id = Guid.NewGuid(), DateTime = orderRequest.DateTime, Status = 0, IdUser = orderRequest.UserId };
            
            double totalPrice = 0;
            List<Product> lstProducts = new List<Product>();
            List<OrderProducts> lstOrderProducts = new List<OrderProducts>();
            foreach (var orderProduct in orderRequest.OrderProducts)
            {
                var product = context.Products.Where(p => p.Id.Equals(orderProduct.IdProduct)).FirstOrDefault();
                if (product == null)
                    return Results.BadRequest("Pedido inválido.");

                if (order.OrderProducts == null)
                    order.OrderProducts = new List<OrderProducts>();

                lstProducts.Add(product);

                orderProduct.UnitPrice = product.Price;
                orderProduct.IdOrder = order.Id;
                totalPrice += product.Price * orderProduct.Amount;
                lstOrderProducts.Add(orderProduct);
                context.OrderProducts.Add(orderProduct);
            }
            order.OrderPrice = totalPrice;
            context.Orders.Add(order);
            context.SaveChanges();

            string orderData = GetOrderData(order, lstProducts, user, lstOrderProducts);
            RabbitMessage rabbitMessage = new RabbitMessage { Id = order.Id, ClientName = user.UserName, OrderData = orderData };
            rabbitMessageService.SendMessage(rabbitMessage);

            return Results.Created($"/order/{order.Id}", order.Id);
        }

        private static string GetOrderData(Order order, List<Product> products, IdentityUser user, List<OrderProducts> orderProducts)
        {
            string result = "";
            result += "Codigo Pedido: " + order.Id.ToString() + "\n";
            result += "Cliente: " + user.UserName + "\n";
            foreach (OrderProducts orderProduct in orderProducts)
            {
                Product product = products.Where(p => p.Id.Equals(orderProduct.IdProduct)).FirstOrDefault();
                result += product.Name + " Valor Unidade:" + product.Price + " Qtd: " + orderProduct.Amount + "\n";
            }
            result += "Valor total: " + order.OrderPrice + "\n";

            return result;
        }
    }
}
