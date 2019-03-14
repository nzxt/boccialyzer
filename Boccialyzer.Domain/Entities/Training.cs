using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Тренування
    /// </summary>
    public class Training : BaseEntity, IEntity
    {
        public Training()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Дата тренування
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// Користувач системи
        /// </summary>
        public Guid AppUserId { get; set; }
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<TrainingBall> TrainingBalls { get; set; } = new Collection<TrainingBall>();

    }
}
