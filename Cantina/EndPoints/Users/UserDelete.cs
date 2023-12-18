using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Users
{
    public class UserDelete
    {
        public static string Template => "/users/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
        {
            var user = context.Users
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (user == null)
            {
                return Results.Ok();
            }

            context.Remove(user);
            context.SaveChanges();

            return Results.Ok();
        }
    }
}
