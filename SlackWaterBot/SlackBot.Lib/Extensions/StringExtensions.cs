using System;
using System.Text.RegularExpressions;

namespace SlackBot.Lib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check whether a string contains other one, ignoring case
        /// </summary>
        /// <param name="input"></param>
        /// <param name="search">string to be find</param>
        /// <returns></returns>
        public static bool ContainsIgnoringCase(this string input, string search)
        {
            return input.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Check whether input string is a number or a noun.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumber(this string input)
        {
            int parser;
            return int.TryParse(input, out parser);
        }

        /// <summary>
        /// Check whether a string is equal other one, ignoring case
        /// </summary>
        /// <param name="input"></param>
        /// <param name="search">string to be find</param>
        /// <returns></returns>
        public static bool EqualsIgnoringCase(this string input, string search)
        {
            return input.Equals(search, StringComparison.OrdinalIgnoreCase);
        }

        public static string RemoveManySpaces(this string input)
        {
            var regex = new Regex("[ ]{2,}", RegexOptions.None);

            if (!string.IsNullOrEmpty(input))
                return regex.Replace(input, " ").TrimEnd().TrimStart().Trim();
            else
                return input;
        }
    }
}
