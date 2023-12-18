using CantinaWebAPI.Infra.Data;

namespace CantinaWebAPI.EndPoints.Products
{
    public class ProductGetAll
    {
        public static string Template => "/products/";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(ApplicationDbContext context)
        {
            var products = context.Products.ToList();
            if (!products.Any())
            {
                return Results.NotFound("Não foram encontrados Produtos.");
            }
            var productsResponse = products.Select(p => new ProductResponse { Id = p.Id, Description = p.Description, Name = p.Name, Price = p.Price });


            List<ProductResponse> lstProductResponse = new List<ProductResponse>();

            foreach(var product in productsResponse)
            {

                lstProductResponse.Add(product);
            }

            return Results.Ok(lstProductResponse);
        }
    }
}
