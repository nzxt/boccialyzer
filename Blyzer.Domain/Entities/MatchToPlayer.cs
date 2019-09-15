using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Relation between match and players
    /// </summary>
    public class MatchToPlayer : LinkToPlayer
    {
        /// <summary>
        /// Is substitute player?
        /// </summary>
        public bool IsSubstitutePlayer { get; set; }
        /// <summary>
        /// Match ID
        /// </summary>
        public Guid? MatchId { get; set; }
        /// <summary>
        /// Match
        /// </summary>
        [Obsolete]
        public virtual Match Match { get; set; }
    }
}
