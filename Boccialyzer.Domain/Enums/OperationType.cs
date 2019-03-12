namespace Boccialyzer.Domain.Enums
{
    /// <summary>
    /// Типи операцій
    /// </summary>
    public enum OperationType : int
    {
        /// <summary>
        /// Не визначено
        /// </summary>
        Undefined,
        /// <summary>
        /// Створення
        /// </summary>
        Create,
        /// <summary>
        /// Читання
        /// </summary>
        Read,
        /// <summary>
        /// Модифікація
        /// </summary>
        Update,
        /// <summary>
        /// Видалення
        /// </summary>
        Delete
    }
}