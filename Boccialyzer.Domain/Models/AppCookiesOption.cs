namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Налаштування Cookies
    /// </summary>
    public class AppCookiesOption
    {
        /// <summary>
        /// Час дії
        /// </summary>
        public int ExpireTimeSpan { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public AppCookiesPassword Password { get; set; }
        /// <summary>
        /// Блокування
        /// </summary>
        public AppCookiesLockout Lockout { get; set; }
        /// <summary>
        /// Користувач
        /// </summary>
        public AppCookiesUser User { get; set; }
        /// <summary>
        /// Аутентифікація
        /// </summary>
        public AppCookiesSignIn SignIn { get; set; }
    }

    /// <summary>
    /// Налаштування Cookies Password
    /// </summary>
    public class AppCookiesPassword
    {
        /// <summary>
        /// Чи обов'язкові цифри
        /// </summary>
        public bool RequireDigit { get; set; }
        /// <summary>
        /// Мінімальна кількість символів
        /// </summary>
        public int RequiredLength { get; set; }
        /// <summary>
        /// Чи обов'язкові спецсимволи
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; }
        /// <summary>
        /// Чи обов'язкові заглавні символи
        /// </summary>
        public bool RequireUppercase { get; set; }
        /// <summary>
        /// Чи обов'язкові строчні символи
        /// </summary>
        public bool RequireLowercase { get; set; }
        /// <summary>
        /// Кількість унікальних символів
        /// </summary>
        public int RequiredUniqueChars { get; set; }
    }

    /// <summary>
    /// Налаштування Cookies Lockout
    /// </summary>
    public class AppCookiesLockout
    {
        /// <summary>
        /// Час блокування
        /// </summary>
        public int DefaultLockoutTimeSpan { get; set; }
        /// <summary>
        /// Кількість спроб
        /// </summary>
        public int MaxFailedAccessAttempts { get; set; }
        /// <summary>
        /// Чи можлива аутентифікація нових користувачів
        /// </summary>
        public bool AllowedForNewUsers { get; set; }
    }

    /// <summary>
    /// Налаштування Cookie sUser
    /// </summary>
    public class AppCookiesUser
    {
        /// <summary>
        /// Чи необхідно перевіряти унікальність пошти
        /// </summary>
        public bool RequireUniqueEmail { get; set; }
    }

    /// <summary>
    /// Налаштування Cookies SignIn
    /// </summary>
    public class AppCookiesSignIn
    {
        /// <summary>
        /// Чи обов'язкове підтвердження пошти
        /// </summary>
        public bool RequireConfirmedEmail { get; set; }
        /// <summary>
        /// Чи обов'язкове підтвердження телефону
        /// </summary>
        public bool RequireConfirmedPhoneNumber { get; set; }
    }
}
