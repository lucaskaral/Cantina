using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Products
{
    public class ProductDelete
    {
        public static string Template => "/products/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
        {
            var product = context.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (product == null)
            {
                return Results.Ok();
            }

            context.Remove(product);
            context.SaveChanges();

            return Results.Ok();
        }
    }
}
