using System;

namespace Boccialyzer.Domain
{
    /// <summary>
    /// Інтерфейс базової сутності
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// Дата і час заведення
        /// </summary>
        DateTime? CreatedOn { get; set; }
        /// <summary>
        /// Дата та час редагування
        /// </summary>
        DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// Користувач системи, що створив запис
        /// </summary>
        Guid? CreatedBy { get; set; }
        /// <summary>
        /// Користувач системи, що модифікував запис
        /// </summary>
        Guid? UpdatedBy { get; set; }
    }
}
