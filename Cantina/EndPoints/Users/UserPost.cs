using CantinaWebAPI.Domain.Users;
using CantinaWebAPI.Infra.Data;

namespace CantinaWebAPI.EndPoints.Users
{
    public class UserPost
    {
        public static string Template => "/users";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(UserRequest userRequest, ApplicationDbContext context)
        {
            var user = new User(userRequest.Name, userRequest.Email, userRequest.Password,
                             userRequest.Identification, DateOnly.Parse(userRequest.DateOfBirth)
                        );

            if (!user.IsValid)
            {
                return Results.ValidationProblem(user.Notifications.ConvertToProblemDetails());
            }



            var userAux = context.Users
                .Where(user => user.Identification == userRequest.Identification)
                .ToList();

            if (userAux.Count > 0) 
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"CPF Já cadastrado." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }

            userAux = context.Users
                .Where(user => user.Email == userRequest.Email)
                .ToList();

            if (userAux.Count > 0)
            {
                Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
                string[] messages = new string[] { $"E-mail Já cadastrado." };
                errors.Add("errors", messages);
                return Results.ValidationProblem(errors);
            }

            context.Users.Add(user);
            context.SaveChanges();

            return Results.Created($"/users/{user.Id}", user.Id);
        }

    }
}
