using System;
using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Domain
{
    /// <summary>
    /// Інтерфейс базової сутності
    /// </summary>
    public interface ILogEntity
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// Дата і час створення
        /// </summary>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// Користувач системи
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// IP-адреса
        /// </summary>
        string IpAddress { get; set; }
        /// <summary>
        /// Дата та час сповіщення
        /// </summary>
        DateTime EventDate { get; set; }
        /// <summary>
        /// Рівень сповіщення
        /// </summary>
        EventLevel EventLevel { get; set; }
        /// <summary>
        /// Текст сповіщення
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// Помилка
        /// </summary>
        string Exception { get; set; }
    }
}
