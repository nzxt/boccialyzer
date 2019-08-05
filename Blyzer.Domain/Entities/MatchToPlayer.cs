using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Зв'язок гравців з матчами
    /// </summary>
    public class MatchToPlayer : LinkToPlayer
    {
        /// <summary>
        /// Гравець для заміни?
        /// </summary>
        public bool IsSubstitutePlayer { get; set; }
        /// <summary>
        /// Ідентифікатор матчу
        /// </summary>
        public Guid? MatchId { get; set; }
        /// <summary>
        /// Матч
        /// </summary>
        [Obsolete]
        public virtual Match Match { get; set; }
    }
}
