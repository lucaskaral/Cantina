using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace CantinaWebAPI.EndPoints.Users
{
    public class EntityUserPost
    {
        public static string Template => "/entityuser";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(UserRequest userRequest, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser { UserName = userRequest.Email, Email = userRequest.Email };
            var result = userManager.CreateAsync(user, userRequest.Password).Result;

            if (!result.Succeeded)
            {
                return Results.BadRequest(result.Errors.First());
            }
            return Results.Created($"/users/{user.Id}", user.Id);
        }
    }
}
