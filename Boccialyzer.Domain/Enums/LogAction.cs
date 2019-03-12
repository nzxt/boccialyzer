namespace Boccialyzer.Domain.Enums
{
    /// <summary>
    /// Статуси дій для логгінгу
    /// </summary>
    public enum LogAction : int
    {
        /// <summary>
        /// Увійшов у систему
        /// </summary>
        LogIn,
        /// <summary>
        /// Вийшов з системи
        /// </summary>
        LogOut,
        /// <summary>
        /// Почав прослуховування
        /// </summary>
        ListenStart,
        /// <summary>
        /// Закінчив прослуховування
        /// </summary>
        ListenStop
    }
}