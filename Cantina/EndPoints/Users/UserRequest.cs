
namespace CantinaWebAPI.EndPoints.Users
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Identification { get; set; }//CPF
        public string DateOfBirth { get; set; }
    }
}
