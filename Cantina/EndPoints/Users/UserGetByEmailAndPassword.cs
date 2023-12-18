using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace CantinaWebAPI.EndPoints.Users
{
    public class UserGetByEmailAndPassword
    {
        public static string Template => "/login/";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromBody]LoginRequest loginRequest, ApplicationDbContext context)
        {
            var user = context.Users
                .Where(user => user.Email == loginRequest.Email && user.Password == loginRequest.Password)
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
