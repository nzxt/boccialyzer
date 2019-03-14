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
            DateTimeStamp = DateTime.UtcNow;
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Дата та час тренування
        /// </summary>
        public DateTime DateTimeStamp { get; set; }
        /// <summary>
        /// Користувач системи
        /// </summary>
        [Required]
        public Guid AppUserId { get; set; }
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<TrainingBall> TrainingBalls { get; set; } = new Collection<TrainingBall>();

    }
}
