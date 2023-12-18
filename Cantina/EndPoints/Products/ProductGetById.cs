using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Products
{
    public class ProductGetById
    {
        public static string Template => "/products/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
        {
            var product = context.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (product == null)
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"Produto não encontrado." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }

            var productResponse = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };


            return Results.Ok(productResponse);
        }
    }
}
