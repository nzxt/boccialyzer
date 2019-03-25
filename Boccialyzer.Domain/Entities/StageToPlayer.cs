using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Зв'язок гравців з етапами
    /// </summary>
    public class StageToPlayer : LinkToPlayers
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public StageToPlayer()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор етапу
        /// </summary>
        [Required]
        public Guid StageId { get; set; }
    }
}
