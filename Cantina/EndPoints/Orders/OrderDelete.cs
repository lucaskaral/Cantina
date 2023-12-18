using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Orders
{
    public class OrderDelete
    {
        public static string Template => "/orders/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
        {
            var order = context.Orders
                .Where(o=> o.Id == id)
                .FirstOrDefault();

            if (order == null)
            {
                return Results.Ok();
            }

            context.Remove(order);
            context.SaveChanges();

            return Results.Ok();
        }
    }
}
