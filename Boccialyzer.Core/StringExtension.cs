using System;
using System.Text;
using Boccialyzer.Domain.Enums;
using Serilog;

namespace Boccialyzer.Core
{
    /// <summary>
    /// String Extension
    /// </summary>
    public static class StringExtension
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

        public static Guid ToGuid(this string str)
        {
            if (Guid.TryParse(str, out var itemId))
                return itemId;
            return default(Guid);
        }

        #endregion
    }
}
