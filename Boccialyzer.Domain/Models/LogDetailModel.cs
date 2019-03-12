using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Деталі для логера
    /// </summary>
    public class LogDetail
    {
        /// <summary>
        /// LogDetail constructor
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="result"></param>
        public LogDetail(string caption, LogOperationResult result)
        {
            OperationCaption = caption;
            OperationResult = result;
        }
        /// <summary>
        /// Сповіщення
        /// </summary>
        public string OperationCaption { get; set; }
        /// <summary>
        /// Результат операції
        /// </summary>
        public LogOperationResult OperationResult { get; set; }
    }
}
