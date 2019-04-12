namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Модель токенів
    /// </summary>
    public class TokensModel
    {
        /// <summary>
        /// Токен доступу
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Токен оновлення
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Час дії токену доступа
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
