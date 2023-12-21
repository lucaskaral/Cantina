using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CantinaWebAPI.EndPoints.Clients
{
    public class ClientPost
    {
        public static string Template => "/clients";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static IResult Action(ClientRequest clientRequest, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser { UserName = clientRequest.Name, Email = clientRequest.Email };
            user.UserName = clientRequest.Name;

            var result = userManager.CreateAsync(user, clientRequest.Password).Result;

            if (!result.Succeeded)
            {
                return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
            }

            var claims = new List<Claim>
            {
                new Claim("Name", clientRequest.Name)
            };
            var claimResult = userManager.AddClaimsAsync(user, claims).Result;
            if (!claimResult.Succeeded)
            {
                return Results.BadRequest($"{claimResult.Errors.First()}");
            }
            return Results.Created($"/clients/{user.Id}", user.Id);
        }

    }
}
