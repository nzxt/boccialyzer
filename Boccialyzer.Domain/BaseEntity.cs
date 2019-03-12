using System;

namespace Boccialyzer.Domain
{
    /// <summary>
    /// Базова сутність
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// BaseEntity constructor
        /// </summary>
        protected BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
            CreatedBy = default(Guid);
        }
        /// <summary>
        /// Дата і час заведення
        /// </summary>
        [Obsolete]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// Дата та час редагування
        /// </summary>
        [Obsolete]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// Користувач системи, що створив запис
        /// </summary>
        [Obsolete]
        public Guid? CreatedBy { get; set; }
        /// <summary>
        /// Користувач системи, що модифікував запис
        /// </summary>
        [Obsolete]
        public Guid? UpdatedBy { get; set; }
    }
}
