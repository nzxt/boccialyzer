using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Системні ролі
    /// </summary>
    public class AppRole : IdentityRole<Guid>
    {
        #region AppRole constructor

        /// <summary>
        /// AppRole constructor
        /// </summary>
        public AppRole() : base()
        {
            CreatedOn = DateTime.UtcNow;
            CreatedBy = default(Guid);
        }

        #endregion
        /// <summary>
        /// Дата і час заведення
        /// </summary>
        [Obsolete]
        public DateTime CreatedOn { get; set; }
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
        /// <summary>
        /// Опис ролі
        /// </summary>
        [Required]
        public string Caption { get; set; }
        /// <summary>
        /// За замовчуванням
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Системна?
        /// </summary>
        public bool IsSystem { get; set; }
        /// <summary>
        /// Адміністратор?
        /// </summary>
        public bool IsAdministrator { get; set; }
        /// <summary>
        /// Суперюзер?
        /// </summary>
        public bool IsSuperUser { get; set; }
        /// <summary>
        /// Експерт?
        /// </summary>
        public bool IsExpert { get; set; }
        /// <summary>
        /// Менеджер?
        /// </summary>
        public bool IsManager { get; set; }
        /// <summary>
        /// Наш співробітник?
        /// </summary>
        public bool IsOwner { get; set; }
    }
}