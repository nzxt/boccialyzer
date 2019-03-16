using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Зв'язок гравців з матчами/тренуваннями
    /// </summary>
    public class MatchToPlayer : BaseEntity, IEntity
    {
        public MatchToPlayer()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSubstitutePlayer { get; set; }
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
        /// <summary>
        /// Ідентифікатор тренування
        /// </summary>
        public Guid? TrainingId { get; set; }
        /// <summary>
        /// Ідентифікатор матчу
        /// </summary>
        public Guid? MatchId { get; set; }


        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<Ball> Balls { get; set; } = new Collection<Ball>();

    }
}
