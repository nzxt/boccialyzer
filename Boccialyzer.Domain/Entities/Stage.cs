using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Період гри
    /// </summary>
    public class Stage : BaseEntity, IEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Stage()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Ідентифікатор матчу
        /// </summary>
        [Required]
        public Guid MatchId { get; set; }
        /// <summary>
        /// Порядковий номер у грі
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// З порушенням?
        /// </summary>
        public bool IsDisrupted { get; set; }
        /// <summary>
        /// Тай-брейк?
        /// </summary>
        public bool IsTieBreak { get; set; }

        /// <summary>
        /// Рахунок червоних
        /// </summary>
        public int ScoreRed { get; set; }
        /// <summary>
        /// Рахунок синіх
        /// </summary>
        public int ScoreBlue { get; set; }
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<Ball> Balls { get; set; } = new Collection<Ball>();
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<StageToPlayer> StageToPlayers { get; set; } = new Collection<StageToPlayer>();
    }
}
