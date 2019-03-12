namespace Boccialyzer.Domain.Enums
{
    /// <summary>
    /// Рівні сповіщень
    /// </summary>
    public enum EventLevel : int
    {
        /// <summary>
        /// Всі сповіщення
        /// </summary>
        Verbose,
        /// <summary>
        /// Пошук помилок
        /// </summary>
        Debug,
        /// <summary>
        /// Інформація
        /// </summary>
        Information,
        /// <summary>
        /// Попередження
        /// </summary>
        Warning,
        /// <summary>
        /// Помилка
        /// </summary>
        Error,
        /// <summary>
        /// Фатальна помилка
        /// </summary>
        Fatal
    }
}
