using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Тип турнирів
    /// </summary>
    public class TournamentType : BaseEntity, IBaseEntity
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Назва
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Абревіатура
        /// </summary>
        [Required]
        public string Abbr { get; set; }
        /// <summary>
        /// Чи є офіційним турніром BISFed?
        /// </summary>
        public bool IsBisFed { get; set; } = true;
        /// <summary>
        /// Іконка
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Турнірии
        /// </summary>
        public ICollection<Tournament> Tournaments { get; set; } = new Collection<Tournament>();
    }
}
