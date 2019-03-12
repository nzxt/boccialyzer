namespace Boccialyzer.Domain.Enums
{
    /// <summary>
    /// Тип сповіщення
    /// </summary>
    public enum LogEventTypeDb
    {
        /// <summary>
        /// Не визначено
        /// </summary>
        Undefined,
        /// <summary>
        /// БД: додано без помилок
        /// </summary>
        DbAddOk,
        /// <summary>
        /// БД: помика додавання
        /// </summary>
        DbAddError,
        /// <summary>
        /// БД: модифіковано без помилок
        /// </summary>
        DbUpdateOk,
        /// <summary>
        /// БД: помилка модифікації
        /// </summary>
        DbUpdateError,
        /// <summary>
        /// БД: видалено без помилок
        /// </summary>
        DbDeleteOk,
        /// <summary>
        /// БД: помилка видалення
        /// </summary>
        DbDeleteError
    }
}