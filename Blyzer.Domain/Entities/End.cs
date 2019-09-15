using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// End
    /// </summary>
    public class End : BaseEntity
    {
        /// <summary>
        /// Match Id
        /// </summary>
        [Required]
        public Guid MatchId { get; set; }
        /// <summary>
        /// Match
        /// </summary>
        [Obsolete]
        public virtual Match Match { get; set; }
        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; set; } = 0;
        /// <summary>
        /// Is Disrupted?
        /// </summary>
        public bool IsDisrupted { get; set; } = false;
        /// <summary>
        /// Is TieBreak?
        /// </summary>
        public bool IsTieBreak { get; set; } = false;
        /// <summary>
        /// Score Red
        /// </summary>
        public int ScoreRed { get; set; } = 0;
        /// <summary>
        /// Score Blue
        /// </summary>
        public int ScoreBlue { get; set; } = 0;
        /// <summary>
        /// Average Point Red
        /// </summary>
        public int AvgPointRed { get; set; } = 0;
        /// <summary>
        ///  Average Point Blue
        /// </summary>
        public int AvgPointBlue { get; set; } = 0;
        /// <summary>
        /// Balls
        /// </summary>
        public virtual ICollection<Ball> Balls { get; set; } = new Collection<Ball>();
        /// <summary>
        /// Players
        /// </summary>
        public virtual ICollection<EndToPlayer> EndToPlayers { get; set; } = new Collection<EndToPlayer>();
    }
}
