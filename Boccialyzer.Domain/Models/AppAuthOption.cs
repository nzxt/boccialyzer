using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Налаштування токену
    /// </summary>
    public class AppAuthOption
    {
        /// <summary>
        /// Видавець токену
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Користувач токену
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Ключ шифрування
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// Час існування токену
        /// </summary>
        public int Expiration { get; set; }
        /// <summary>
        /// Генерація симетричного ключа шифрування
        /// </summary>
        /// <returns></returns>
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.SecurityKey));
        }
    }
}
