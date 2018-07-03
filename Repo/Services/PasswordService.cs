using System.Linq;

namespace Repo.Services
{
    public static class PasswordService
    {
        public static string _LOWERCASEALPHABETICAL = GetCharRange('a', 'z', "li"); // "abcdefghjkmnopqrstuvwxyz";
        public static string _UPPERCASEALPHABETICAL = GetCharRange('A', 'Z'); // "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string _NUMERICAL = GetCharRange('0', '9'); // "0123456789";
        public static string _SYMBOL = "@%+!#$?:.-_";
        //public static string _SYMBOL = "@%+!#$^?:.(){}[]~-_`";

        public static string RandomUpperCase(int length) => GetRandomString(_UPPERCASEALPHABETICAL, length);
        public static string RandomLowerCase(int length) => GetRandomString(_LOWERCASEALPHABETICAL, length);
        public static string RandomNumerical(int length) => GetRandomString(_NUMERICAL, length);
        public static string RandomSymbol(int length) => GetRandomString(_SYMBOL, length);

        public static string GetCharRange(char begin, char end, string excludingChars = "")
        {
            var result = string.Empty;
            for (char c = begin; c <= end; c++)
            {
                result += c;
            }

            if (!string.IsNullOrEmpty(excludingChars))
            {
                var inclusiveChars = result.Except(excludingChars).ToArray();
                result = new string(inclusiveChars);
            }
            return result;
        }

        private static string GetRandomString(string possibleChars, int len)
        {
            var result = string.Empty;
            for (var position = 0; position < len; position++)
            {
                var index = RandomService.Next(possibleChars.Length);
                result += possibleChars[index];
            }
            return result;
        }

        public static string GenerateRandomPassword(int numOfLowerCase, int numOfUpperCase, int numOfNumeric, int numOfSymbol)
        {
            string ordered = RandomLowerCase(numOfLowerCase)
                + RandomUpperCase(numOfUpperCase)
                + RandomNumerical(numOfNumeric)
                + RandomSymbol(numOfSymbol);

            string shuffled = new string(ordered.OrderBy(n => RandomService.NextInt32()).ToArray());

            return shuffled;
        }
    }
}
