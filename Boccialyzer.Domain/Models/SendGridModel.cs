namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// SendGrid Model
    /// </summary>
    public class SendGridModel
    {
        /// <summary>
        /// Ім'я користувача
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Api Key
        /// </summary>
        public string ApiKey { get; set; }
    }
}
