using Boccialyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Гравці
    /// </summary>
    public class Player : BaseEntity, IEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Player()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Ім'я та прізвище
        /// </summary>
        [Required]
        public string FullName { get; set; }
        /// <summary>
        /// Класифікація
        /// </summary>
        public PlayerClassification PlayerClassification { get; set; }
        /// <summary>
        /// Країна
        /// </summary>
        public Guid? CountryId { get; set; }

        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<MatchToPlayer> MatchToPlayers { get; set; } = new Collection<MatchToPlayer>();

    }
}
