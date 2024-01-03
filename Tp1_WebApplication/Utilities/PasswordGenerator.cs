using System.Text;

namespace Tp1_WebApplication.Utilities
{
    public class PasswordGenerator
    {
        private static Random RANDOM = new();

        private const string LOWERCASES = "abcdefghijklmnopqrstuvwxyz";
        private const string UPPERCASES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DIGITS = "0123456789";
        private const string SPECIALS = "!@#$%&*";

        private const int LENGTH_MIN = 10;
        private const int LOWERCASE_MIN = 1;
        private const int UPPERCASE_MIN = 1;
        private const int DIGITS_MIN = 1;
        private const int SPECIAL_MIN = 1;

        public static string Generate()
        {
            var password = new StringBuilder();

            for (int i = 0; i < LOWERCASE_MIN; i++)
            {
                password.Append(LOWERCASES[RANDOM.Next(LOWERCASES.Length)]);
            }

            for (int i = 0; i < UPPERCASE_MIN; i++)
            {
                password.Append(UPPERCASES[RANDOM.Next(UPPERCASES.Length)]);
            }

            for (int i = 0; i < DIGITS_MIN; i++)
            {
                password.Append(DIGITS[RANDOM.Next(DIGITS.Length)]);
            }

            for (int i = 0; i < SPECIAL_MIN; i++)
            {
                password.Append(SPECIALS[RANDOM.Next(SPECIALS.Length)]);
            }

            while (password.Length < LENGTH_MIN)
            {
                password.Append(LOWERCASES[RANDOM.Next(LOWERCASES.Length)]);
            }

            return new string(password.ToString().ToCharArray()
                .OrderBy(x => RANDOM.Next()).ToArray()
            );
        }
    }
}
