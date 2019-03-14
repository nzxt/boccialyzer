namespace Boccialyzer.Web
{
    /// <summary>
    /// Поточний стан
    /// </summary>
    public static class AppState
    {
        /// <summary>
        /// Сталась фатальна помилка
        /// </summary>
        public static bool FatalError { get; set; }
        /// <summary>
        /// Сталась помилка
        /// </summary>
        public static bool Error { get; set; }
        /// <summary>
        /// Строка підключення до БД
        /// </summary>
        public static string ConnectionString { get; set; }
        /// <summary>
        /// Тимчасове відключення логгінгу
        /// </summary>
        public static bool LoggingDisable { get; set; }
    }
}
