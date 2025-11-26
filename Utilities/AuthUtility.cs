using System.Security.Claims;

namespace Project_Planner_API.Utilities
{
    public class AuthUtility
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool CheckPassword(string hashedPassword, string enteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }

        public static Guid GetUserId(ClaimsPrincipal user)
        {
            string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }

            return new Guid(userId);
        }
    }
}
