namespace Boccialyzer.Domain.Enums
{ 
    /// <summary>
    /// Результат виконання операції
    /// </summary>
    public enum OperationResult : int
    {
        /// <summary>
        /// Не визначено
        /// </summary>
        NotDefined,
        /// <summary>
        /// Успіх
        /// </summary>
        Ok,
        /// <summary>
        /// Попередження
        /// </summary>
        Warning,
        /// <summary>
        /// Помилка
        /// </summary>
        Error
    }
}
