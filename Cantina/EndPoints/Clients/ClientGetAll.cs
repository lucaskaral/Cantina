using CantinaWebAPI.EndPoints.Clients;
using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace CantinaWebAPI.EndPoints.Clients
{
    public class ClientGetAll
    {
        public static string Template => "/clients/";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(UserManager<IdentityUser> userManager)
        {
            var users = userManager.Users.ToList();
            var clients = new List<ClientResponse>();
            foreach (var user in users)
            {
                var claims = userManager.GetClaimsAsync(user).Result;
                var claimName = claims.FirstOrDefault(c => c.Type == "Name");
                var userName = claimName != null ? claimName.Value : string.Empty;
                clients.Add(new ClientResponse(user.Email, userName));
            }
            return Results.Ok(clients);
        }
    }
}
