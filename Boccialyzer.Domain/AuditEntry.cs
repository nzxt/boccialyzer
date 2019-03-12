using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.LogEntities;
using Boccialyzer.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boccialyzer.Domain
{
    /// <summary>
    /// Audit trail entity
    /// </summary>
    public class AuditEntry
    {
        #region # AuditEntry constructor

        /// <summary>
        /// AuditEntry constructor
        /// </summary>
        ///// <param name="entry"></param>
        public AuditEntry(string tableName)//EntityEntry entry)
        {
            EventDate = DateTime.UtcNow;
            TableName = tableName;
            OperationType = OperationType.Undefined;
        }

        #endregion

        /// <summary>
        /// Дата і час створення
        /// </summary>
        public DateTime EventDate { get; set; }
        /// <summary>
        /// Тип сповіщення
        /// </summary>
        public LogEventTypeDb EventType { get; set; }
        /// <summary>
        /// Table name
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Key values
        /// </summary>
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        /// <summary>
        /// Old values
        /// </summary>
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        /// <summary>
        /// New values
        /// </summary>
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        /// <summary>
        /// Temporary Properties
        /// </summary>
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        /// <summary>
        /// Message for Serilog
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Operation Type
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// Has Temporary Properties?
        /// </summary>
        public bool HasTemporaryProperties => TemporaryProperties.Any();


        /// <summary>
        /// Convert to AuditLog
        /// </summary>
        /// <returns></returns>
        public string ToAudit()
        {
            var result = new AuditLog
            {
                TableName = TableName,
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? string.Empty : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? string.Empty : JsonConvert.SerializeObject(NewValues),
                OperationType = OperationType,
                OperationResult = OperationResult.Ok
            };

            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// Convert to string for Serilog
        /// </summary>
        /// <returns></returns>
        public new string ToString()
        {
            return $"Користувач  Table: {this.TableName}, Key: {JsonConvert.SerializeObject(KeyValues)}";
        }

        /// <summary>
        /// Convert to LogDbEvent
        /// </summary>
        /// <returns></returns>
        public LogDbEvent ToLogDbEvent()
        {
            return new LogDbEvent
            {
                EventDate = EventDate,
                EventType = EventType,
                TableName = TableName,
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? string.Empty : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? string.Empty : JsonConvert.SerializeObject(NewValues),
                OperationType = OperationType,
                OperationResult = OperationResult.Ok,
                Message = Message
            };
        }
    }
}
