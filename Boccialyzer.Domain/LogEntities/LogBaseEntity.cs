using Boccialyzer.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.LogEntities
{
    /// <summary>
    /// Базова сутність логгера
    /// </summary>
    public abstract class LogBaseEntity
    {
        /// <summary>
        /// LogBaseEntity constructor
        /// </summary>
        protected LogBaseEntity()
        {
            Id = Guid.NewGuid();
            UserName = "Unknow";
            IpAddress = "000.000.000.000";
        }

        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Дата і час створення
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Користувач системи
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// IP-адреса
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Дата та час сповіщення
        /// </summary>
        public DateTime EventDate { get; set; }
        /// <summary>
        /// Рівень сповіщення
        /// </summary>
        public EventLevel EventLevel { get; set; }
        /// <summary>
        /// Текст сповіщення
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Помилка
        /// </summary>
        public string Exception { get; set; }
    }
}
