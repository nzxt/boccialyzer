namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Поточні налаштування
    /// </summary>
    public class AppOptionModel
    {
        /// <summary>
        /// Рядок підключення до БД
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// URL для підтвердження email
        /// </summary>
        public string ConfirmEmailUrl { get; set; }
        /// <summary>
        /// Налаштування SendGrid
        /// </summary>
        public SendGridModel SendGrid { get; set; }
    }
}
