using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Зв'язок гравців з матчами/тренуваннями
    /// </summary>
    public abstract class LinkToPlayers : BaseEntity, IEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LinkToPlayers()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// BIB
        /// </summary>
        public int Bib { get; set; }
        /// <summary>
        /// Номер боксу
        /// </summary>
        public int Box { get; set; }
        /// <summary>
        /// Гравці
        /// </summary>
        public Guid PlayerId { get; set; }
    }
}
