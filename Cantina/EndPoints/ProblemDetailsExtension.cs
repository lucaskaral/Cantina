using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace CantinaWebAPI.EndPoints
{
    public static class ProblemDetailsExtension
    {
        public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
        {
            Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
            List<string> list = new List<string>();
            foreach(var notification in notifications)
            {
                list.Add(notification.Message);
            }
            dictionary.Add("errors", list.ToArray());
            return dictionary;
        }

        public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
        {
            var dictionary = new Dictionary<string, string[]>();
            dictionary.Add("Error", error.Select(e => e.Description).ToArray());
            return dictionary;
        }
    }
}
