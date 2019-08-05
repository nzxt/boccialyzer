using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Зв'язок гравців з періодами гри
    /// </summary>
    public class EndToPlayer : LinkToPlayer
    {
        /// <summary>
        /// Ідентифікатор періоду гри
        /// </summary>
        public Guid? EndId { get; set; }
        /// <summary>
        /// Період гри
        /// </summary>
        [Obsolete]
        public virtual End End { get; set; }
    }
}
