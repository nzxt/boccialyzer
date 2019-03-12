using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// AuditLog entity
    /// </summary>
    public class AuditLog
    {
        /// <summary>
        /// Table Name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Key Values
        /// </summary>
        public string KeyValues { get; set; }

        /// <summary>
        /// Old Values
        /// </summary>
        public string OldValues { get; set; }

        /// <summary>
        /// New Values
        /// </summary>
        public string NewValues { get; set; }
        /// <summary>
        /// Operation Type
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// Operation Result
        /// </summary>
        public OperationResult OperationResult { get; set; }
    }
}
