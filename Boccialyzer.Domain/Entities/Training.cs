using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Тренування
    /// </summary>
    public class Training : BaseEntity, IEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
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
        /// Тип тренування
        /// </summary>
        public TrainingType TrainingType { get; set; }
        /// <summary>
        /// Оцінка тренування
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// Користувач системи
        /// </summary>
        [Required]
        public Guid AppUserId { get; set; }
        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<Ball> Balls { get; set; } = new Collection<Ball>();
        /// <summary>
        /// Тренувальна гра
        /// </summary>
        public virtual ICollection<Match> Matches { get; set; } = new Collection<Match>();
    }
}
