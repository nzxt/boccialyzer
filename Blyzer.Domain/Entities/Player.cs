using Blyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Player
    /// </summary>
    public class Player : BaseEntity
    {
        /// <summary>
        /// Player fullname
        /// </summary>
        [Required]
        public string FullName { get; set; }
        /// <summary>
        /// Classification
        /// </summary>
        public PlayerClassification PlayerClassification { get; set; } = PlayerClassification.None;
        /// <summary>
        /// Country ID
        /// </summary>
        public Guid? CountryId { get; set; }
        /// <summary>
        /// Is BisFed player?
        /// </summary>
        public bool IsBisFed { get; set; } = false;
        /// <summary>
        /// Matches
        /// </summary>
        public virtual ICollection<MatchToPlayer> MatchToPlayers { get; set; } = new Collection<MatchToPlayer>();
        /// <summary>
        /// Balls
        /// </summary>
        public virtual ICollection<Ball> Balls { get; set; } = new Collection<Ball>();
    }
}
