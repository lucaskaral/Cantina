using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Orders
{
    public class OrderGetById
    {
        public static string Template => "/orders/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
        {
            var order = context.Orders
                .Where(o => o.Id == id)
                .FirstOrDefault();

            if (order == null)
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"Pedido não encontrado." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }

            var orderResponse = new OrderResponse
            {
                Id = order.Id,
                DateTime = order.DateTime,
            };


            return Results.Ok(orderResponse);
        }
    }
}
