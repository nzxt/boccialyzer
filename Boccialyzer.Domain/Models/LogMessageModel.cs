using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Сповіщення лога
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// Відмітка дати та часу
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Рівень сповіщення
        /// </summary>
        public LogLevel Level { get; set; }
        /// <summary>
        /// Ім'я текстового файлу
        /// </summary>
        public string TextFileName { get; set; }
        /// <summary>
        /// Ім'я інформаційного файлу
        /// </summary>
        public string InfoFileName { get; set; }
        /// <summary>
        /// Сповіщення
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Хеш сповіщення
        /// </summary>
        public string MessageHash { get; set; }
        /// <summary>
        /// Деталі логу
        /// </summary>
        public List<LogDetail> LogDetails { get; set; }
    }
}
