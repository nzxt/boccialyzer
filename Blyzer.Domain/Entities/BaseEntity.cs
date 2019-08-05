using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Інтерфейс базової сутності
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Identifier
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// Дата і час заведення
        /// </summary>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// Дата та час редагування
        /// </summary>
        DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// Користувач системи, що створив запис
        /// </summary>
        Guid CreatedBy { get; set; }
        /// <summary>
        /// Користувач системи, що модифікував запис
        /// </summary>
        Guid? UpdatedBy { get; set; }
        /// <summary>
        /// Is it public access?
        /// </summary>
        bool IsPublic { get; set; }
    }

    /// <summary>
    /// Base Entity
    /// </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Дата і час заведення
        /// </summary>
        [Obsolete]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Дата та час редагування
        /// </summary>
        [Obsolete]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// Користувач системи, що створив запис
        /// </summary>
        [Obsolete]
        public Guid CreatedBy { get; set; } = default(Guid);
        /// <summary>
        /// Користувач системи, що модифікував запис
        /// </summary>
        [Obsolete]
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Is it public access?
        /// </summary>
        [Obsolete]
        public bool IsPublic { get; set; } = false;
    }
}
