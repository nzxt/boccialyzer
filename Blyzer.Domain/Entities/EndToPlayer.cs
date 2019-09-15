using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Relation between End and Player
    /// </summary>
    public class EndToPlayer : LinkToPlayer
    {
        /// <summary>
        /// End ID
        /// </summary>
        public Guid? EndId { get; set; }
        /// <summary>
        /// End
        /// </summary>
        [Obsolete]
        public virtual End End { get; set; }
    }
}
