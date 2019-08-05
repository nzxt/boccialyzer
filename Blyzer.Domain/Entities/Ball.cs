using Blyzer.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Ball
    /// </summary>
    public class Ball : BaseEntity, IBaseEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Ball()
        {
            Id = Guid.NewGuid();
            IsDeadBall = false;
            IsPenalty = false;
            DeadBallType = DeadBallType.None;
        }
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Number in period
        /// </summary>
        [Index]
        public int Index { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        [Index]
        public int Rating { get; set; }
        /// <summary>
        /// Is it jack-ball?
        /// </summary>
        public bool IsJack { get; set; }
        /// <summary>
        /// Is it penalty ball?
        /// </summary>
        public bool IsPenalty { get; set; }
        /// <summary>
        /// Is it dead-ball?
        /// </summary>
        public bool IsDeadBall { get; set; }
        /// <summary>
        /// Dead-ball type
        /// </summary>
        public DeadBallType DeadBallType { get; set; }
        /// <summary>
        /// Тип кидка
        /// </summary>
        [Index]
        public ShotType ShotType { get; set; }
        /// <summary>
        /// Ігрова зона
        /// </summary>
        [Index]
        public Box FromBox { get; set; }

        /// <summary>
        /// Дистанція
        /// </summary>
        [Index]
        public float Distance { get; set; } = 0;
        /// <summary>
        /// Період гри
        /// </summary>
        [Required]
        public Guid EndId { get; set; }
        /// <summary>
        /// Гравець
        /// </summary>
        [Required]
        public Guid PlayerId { get; set; }
        /// <summary>
        /// Coordinate X
        /// </summary>
        public float CoordinateX { get; set; } = 0;
        /// <summary>
        /// Coordinate Y
        /// </summary>
        public float CoordinateY { get; set; } = 0;
    }
}
