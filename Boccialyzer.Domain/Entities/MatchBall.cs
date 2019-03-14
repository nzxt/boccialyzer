using System;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// М'ячі у матчі
    /// </summary>
    public class MatchBall : Ball
    {
        /// <summary>
        /// Ідентифікатор матчу
        /// </summary>
        public Guid MatchId { get; set; }
    }
}
