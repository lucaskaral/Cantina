using CantinaWebAPI.Domain.Products;
using CantinaWebAPI.Infra.Data;

namespace CantinaWebAPI.EndPoints.Products
{
    public class ProductPost
    {
        public static string Template => "/products";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(ProductRequest productRequest, ApplicationDbContext context)
        {
            var product = new Product 
                    { Name =  productRequest.Name, Description = productRequest.Description, Price = productRequest.Price };


            if (!product.IsValid)
            {
                return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());
            }



            var productAux = context.Products
                .Where(p => p.Name == productRequest.Name)
                .ToList();

            if (productAux.Count > 0) 
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"Produto Já cadastrado." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }


            context.Products.Add(product);
            context.SaveChanges();

            return Results.Created($"/products/{product.Id}", product.Id);
        }

    }
}
