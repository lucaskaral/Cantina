using CantinaWebAPI.Infra.Data;

namespace CantinaWebAPI.EndPoints.Orders
{
    public class OrderGetAll
    {
        public static string Template => "/orders/";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(ApplicationDbContext context)
        {
            var orders = context.Orders.ToList();
            if (!orders.Any())
            {
                return Results.NotFound("Não foram encontrados Pedidos.");
            }
            var ordersResponse = orders.Select(p => new OrderResponse { Id = p.Id, TotalPrice = p.Status } );


            List<OrderResponse> lstOrderResponse = new List<OrderResponse>();

            foreach(var order in lstOrderResponse)
            {

                lstOrderResponse.Add(order);
            }

            return Results.Ok(lstOrderResponse);
        }
    }
}
