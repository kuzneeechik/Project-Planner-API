using System.Security.Cryptography;

namespace Project_Planner_API.Utilities
{
    public class CodeUtility
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenerateCode()
        {
            var lenth = 8;

            var result = new char[lenth];

            for (int i = 0; i < lenth; i++)
            {
                int currentChar = RandomNumberGenerator.GetInt32(Chars.Length);

                result[i] = Chars[currentChar];
            }

            return new string(result);
        }
    }
}
