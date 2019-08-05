using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Період гри
    /// </summary>
    public class End : BaseEntity, IBaseEntity
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Ідентифікатор матчу
        /// </summary>
        [Required]
        public Guid MatchId { get; set; }
        /// <summary>
        /// Match
        /// </summary>
        [Obsolete]
        public virtual Match Match { get; set; }
        /// <summary>
        /// Порядковий номер у грі
        /// </summary>
        [Index]
        public int Index { get; set; } = 0;
        /// <summary>
        /// З порушенням?
        /// </summary>
        public bool IsDisrupted { get; set; } = false;
        /// <summary>
        /// Тай-брейк?
        /// </summary>
        public bool IsTieBreak { get; set; } = false;
        /// <summary>
        /// Рахунок червоних
        /// </summary>
        public int ScoreRed { get; set; } = 0;
        /// <summary>
        /// Рахунок синіх
        /// </summary>
        public int ScoreBlue { get; set; } = 0;
        /// <summary>
        /// Середня якість кидків червоних
        /// </summary>
        public int AvgPointRed { get; set; } = 0;
        /// <summary>
        /// Середня якість кидків синіх
        /// </summary>
        public int AvgPointBlue { get; set; } = 0;
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<Ball> Balls { get; set; } = new Collection<Ball>();
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<EndToPlayer> EndToPlayers { get; set; } = new Collection<EndToPlayer>();
    }
}
