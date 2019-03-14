using System;
using System.ComponentModel.DataAnnotations;
using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Domain.Entities
{
    public class Ball : BaseEntity, IEntity
    {
        public Ball()
        {
            Id = Guid.NewGuid();
            IsDeadBall = false;
            IsPenalty = false;
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Оцінка
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// Штрафний м'яч ?
        /// </summary>
        public bool IsPenalty { get; set; }
        /// <summary>
        /// М'яч поза грою?
        /// </summary>
        public bool IsDeadBall { get; set; }
        /// <summary>
        /// Типи м'ячів поза грою
        /// </summary>
        public DeadBallType DeadBallType { get; set; }
        /// <summary>
        /// Тип кидка
        /// </summary>
        public ShotType ShotType { get; set; }
        /// <summary>
        /// Ігрова зона
        /// </summary>
        public Box Box { get; set; }
        /// <summary>
        /// Дистанція
        /// </summary>
        public Distance Distance { get; set; }
    }
}
