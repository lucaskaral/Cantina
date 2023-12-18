using CantinaWebAPI.Infra.Data;

namespace CantinaWebAPI.EndPoints.Users
{
    public class UserGetAll
    {
        public static string Template => "/users/";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(ApplicationDbContext context)
        {
            var users = context.Users.ToList();
            if (!users.Any())
            {
                return Results.NotFound("Não foram encontrados usuários.");
            }
            var usersResponse = users.Select(p => new UserResponse { Id = p.Id, Email = p.Email, Identification = p.Identification, Name = p.Name, DateOfBirth = p.DateOfBirth });


            List<UserResponse> lstUserResponse = new List<UserResponse>();

            foreach(var user in usersResponse)
            {

                lstUserResponse.Add(user);
            }

            return Results.Ok(lstUserResponse);
        }
    }
}
