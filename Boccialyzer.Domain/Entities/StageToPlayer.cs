using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Зв'язок гравців з періодами гри
    /// </summary>
    public class StageToPlayer : LinkToPlayers
    {
        /// <summary>
        /// Ідентифікатор періоду гри
        /// </summary>
        //[Required]
        public Guid StageId { get; set; }
        /// <summary>
        /// Період гри
        /// </summary>
        [Obsolete]
        public virtual Stage Stage { get; set; }
    }
}
