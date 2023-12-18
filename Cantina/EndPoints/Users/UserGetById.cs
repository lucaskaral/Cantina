using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Users
{
    public class UserGetById
    {
        public static string Template => "/users/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
        {
            var user = context.Users
                .Where(user => user.Id == id)
                .FirstOrDefault();

            if (user == null)
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"Usuário e/ou senha inválido." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }

            var userResponse = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Identification = user.Identification,
                Name = user.Name,
                DateOfBirth = user.DateOfBirth
            };


            return Results.Ok(userResponse);
        }
    }
}
