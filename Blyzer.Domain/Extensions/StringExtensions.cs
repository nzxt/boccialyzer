using System;
using System.Text.RegularExpressions;

namespace Blyzer.Domain.Extensions
{
    /// <summary>
    /// String Extension
    /// </summary>
    public static class StringExtensions
    {
        #region # ToCamelCase(...)

        /// <summary>
        /// ToCamelCase
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>String in CamelCase</returns>
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();
            }
            return str;
        }

        #endregion
        #region # ToFirstUpperCase(...)

        /// <summary>
        /// ToFirstUpperCase
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>String, First symbol upper</returns>
        public static string ToFirstUpperCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return $"{str.Substring(0, 1).ToUpperInvariant()}{str.Substring(1)}";
            }
            return str;
        }

        #endregion
        #region # ToGuid(...)

        /// <summary>
        /// ToGuid
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>String</returns>
        public static Guid ToGuid(this string str)
        {
            if (Guid.TryParse(str, out var itemId))
                return itemId;
            return default(Guid);
        }

        #endregion
        #region # ToSnakeCase(...)

        /// <summary>
        /// ToSnakeCase
        /// </summary>
        /// <param name="input">String</param>
        /// <returns>String</returns>
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        #endregion
    }
}
