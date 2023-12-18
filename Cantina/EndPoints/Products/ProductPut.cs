using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Products
{
    public class ProductPut
    {
        public static string Template => "/products/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, ProductRequest productRequest, ApplicationDbContext context)
        {
            var product = context.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (product == null)
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"Produto inválido." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }
            product.Name = productRequest.Name;
            product.Description = productRequest.Description;
            product.Price = productRequest.Price;


            if (!product.IsValid)
            {
                return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());
            }

            context.SaveChanges();

            return Results.Ok();
        }
    }
}

