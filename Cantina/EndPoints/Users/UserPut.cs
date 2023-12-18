using CantinaWebAPI.Domain.Users;
using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Users
{
    public class UserPut
    {
        public static string Template => "/users/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, UserRequest userRequest, ApplicationDbContext context)
        {
            var user = context.Users
                .Where(u => u.Id == id)
                .FirstOrDefault();

            if (user == null)
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"Usuário e/ou senha inválido." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }
            user.Name = userRequest.Name;
            user.Email = userRequest.Email;
            user.Password = userRequest.Password;
            user.Identification = userRequest.Identification;
            user.DateOfBirth = DateOnly.Parse(userRequest.DateOfBirth);

            if (!user.IsValid)
            {
                return Results.ValidationProblem(user.Notifications.ConvertToProblemDetails());
            }

            context.SaveChanges();

            return Results.Ok();
        }
    }
}

