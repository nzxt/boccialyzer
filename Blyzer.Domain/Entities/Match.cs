using Blyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Match
    /// </summary>
    public class Match : BaseEntity
    {
        /// <summary>
        /// Date of match
        /// </summary>
        //[Required]
        public DateTime DateTimeStamp { get; set; }
        /// <summary>
        /// Match type
        /// </summary>
        public MatchType MatchType { get; set; } = MatchType.None;
        /// <summary>
        /// Rate
        /// </summary>
        public int Rate { get; set; } = 0;
        /// <summary>
        /// Competition Event
        /// </summary>
        public CompetitionEvent CompetitionEvent { get; set; } = CompetitionEvent.None;
        /// <summary>
        /// Pool stage
        /// </summary>
        public string PoolStage { get; set; }
        /// <summary>
        /// Elimination stage
        /// </summary>
        public EliminationStage EliminationStage { get; set; }
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
        /// Average Point Blue
        /// </summary>
        public int AvgPointBlue { get; set; } = 0;
        /// <summary>
        /// Flag for Red
        /// </summary>
        public string FlagRed { get; set; }
        /// <summary>
        /// Flag for Blue
        /// </summary>
        public string FlagBlue { get; set; }
        /// <summary>
        /// Tournament ID
        /// </summary>
        public Guid? TournamentId { get; set; }

        /// <summary>
        /// Players
        /// </summary>
        public virtual ICollection<MatchToPlayer> MatchToPlayers { get; set; } = new Collection<MatchToPlayer>();
        /// <summary>
        /// Ends
        /// </summary>
        public virtual ICollection<End> Ends { get; set; } = new Collection<End>();
    }
}
