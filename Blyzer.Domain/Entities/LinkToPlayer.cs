using System;
using System.ComponentModel.DataAnnotations;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Зв'язок гравців з матчами/тренуваннями
    /// </summary>
    public abstract class LinkToPlayer : BaseEntity, IBaseEntity
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// BIB
        /// </summary>
        public int Bib { get; set; }
        /// <summary>
        /// Номер боксу
        /// </summary>
        [Required]
        [Index]
        public int Box { get; set; }
        /// <summary>
        /// Гравець
        /// </summary>
        [Required]
        public Guid PlayerId { get; set; }
    }
}
