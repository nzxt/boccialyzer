namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Налаштування Cookies
    /// </summary>
    public class IdentityOption
    {
        /// <summary>
        /// Час дії
        /// </summary>
        public int ExpireTimeSpan { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public IdentityPassword Password { get; set; }
        /// <summary>
        /// Блокування
        /// </summary>
        public IdentityLockout Lockout { get; set; }
        /// <summary>
        /// Користувач
        /// </summary>
        public IdentityUser User { get; set; }
        /// <summary>
        /// Аутентифікація
        /// </summary>
        public IdentitySignIn SignIn { get; set; }
    }

    /// <summary>
    /// Налаштування Cookies Password
    /// </summary>
    public class IdentityPassword
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
    public class IdentityLockout
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
    public class IdentityUser
    {
        /// <summary>
        /// Чи необхідно перевіряти унікальність пошти
        /// </summary>
        public bool RequireUniqueEmail { get; set; }
    }

    /// <summary>
    /// Налаштування Cookies SignIn
    /// </summary>
    public class IdentitySignIn
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
