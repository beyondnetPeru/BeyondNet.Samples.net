using System.Text.RegularExpressions;

namespace NotifyServer.Library.Extension
{
    public static class StringExtensions
    {
        public static bool IsGuid(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            var pattern =
                new Regex(
                    @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

            return pattern.IsMatch(value);
        }
    }
}
