using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Зв'язок гравців з матчами
    /// </summary>
    public class MatchToPlayer : LinkToPlayers
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchToPlayer()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Гравець для заміни?
        /// </summary>
        public bool IsSubstitutePlayer { get; set; }
        /// <summary>
        /// Ідентифікатор матчу
        /// </summary>
        [Required]
        public Guid MatchId { get; set; }
    }
}
