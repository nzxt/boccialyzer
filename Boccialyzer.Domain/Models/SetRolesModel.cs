using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Модель для додавання ролей користувачу
    /// </summary>
    public class SetRolesModel
    {
        /// <summary>
        /// Ідентифікатор користувача
        /// </summary>
        [Required]
        public Guid AppUserId { get; set; }
        /// <summary>
        /// Список ідентифікаторів ролей
        /// </summary>
        [Required]
        public List<Guid> RoleIds { get; set; }
    }
}
