using Blyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Гравець
    /// </summary>
    public class Player : BaseEntity, IBaseEntity
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Ім'я та прізвище
        /// </summary>
        [Required]
        [Index]
        public string FullName { get; set; }
        /// <summary>
        /// Класифікація
        /// </summary>
        [Index]
        public PlayerClassification PlayerClassification { get; set; } = PlayerClassification.None;
        /// <summary>
        /// Країна
        /// </summary>
        public Guid? CountryId { get; set; }
        /// <summary>
        /// Чи є гравцем BISFed?
        /// </summary>
        public bool IsBisFed { get; set; } = false;
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<MatchToPlayer> MatchToPlayers { get; set; } = new Collection<MatchToPlayer>();
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<Ball> Balls { get; set; } = new Collection<Ball>();
    }
}
