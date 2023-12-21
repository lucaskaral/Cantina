using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rabbit.Services;
using Rabbit.Services.Interfaces;

namespace CantinaWebAPI.EndPoints.Orders
{
    public class OrderPut
    {
        private readonly IRabbitMessageService _rabbitMessageService;

        public static string Template => "/orders/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        [Authorize]
        public static IResult Action([FromRoute] Guid id, OrderRequest orderRequest, ApplicationDbContext context)
        {
            var order = context.Orders
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (order == null)
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"Pedido inválido." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }
            order.DateTime  = orderRequest.DateTime;


            if (!order.IsValid)
            {
                return Results.ValidationProblem(order.Notifications.ConvertToProblemDetails());
            }

            context.SaveChanges();

            return Results.Ok();
        }
    }
}

