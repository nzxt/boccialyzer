using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Application role
    /// </summary>
    public class AppRole : IdentityRole<Guid>
    {
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
        public Guid? CreatedBy { get; set; } = default(Guid);

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
        /// Адміністратор?
        /// </summary>
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// Суперюзер?
        /// </summary>
        public bool IsSuperUser { get; set; }

        /// <summary>
        /// Менеджер?
        /// </summary>
        public bool IsManager { get; set; }
        /// <summary>
        /// User Roles
        /// </summary>
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        /// <summary>
        /// Application Role Claim
        /// </summary>
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
    }
}
