using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.LogEntities;

namespace Boccialyzer.Domain.LogEntities
{
    /// <summary>
    /// Сповіщення операцій з БД
    /// </summary>
    public class LogDbEvent : LogBaseEntity, ILogEntity
    {
        /// <summary>
        /// LogDbEvent creator
        /// </summary>
        public LogDbEvent()
        {
            OperationType = OperationType.Undefined;
            OperationResult = OperationResult.NotDefined;
        }
        /// <summary>
        /// Тип сповіщення
        /// </summary>
        public LogEventTypeDb EventType { get; set; }
        /// <summary>
        /// Сутність
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Ідентифікатор запису
        /// </summary>
        public string KeyValues { get; set; }
        /// <summary>
        /// Старе значення
        /// </summary>
        public string OldValues { get; set; }
        /// <summary>
        /// Нове значення
        /// </summary>
        public string NewValues { get; set; }
        /// <summary>
        /// Тип операції
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// Результат виконання операції
        /// </summary>
        public OperationResult OperationResult { get; set; }
    }
}
