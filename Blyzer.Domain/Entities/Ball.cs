using Blyzer.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Ball
    /// </summary>
    public class Ball : BaseEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Ball()
        {
            IsDeadBall = false;
            IsPenalty = false;
            DeadBallType = DeadBallType.None;
        }
        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
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
        /// Shot type
        /// </summary>
        public ShotType ShotType { get; set; }
        /// <summary>
        /// Box
        /// </summary>
        public Box FromBox { get; set; }
        /// <summary>
        /// Distance
        /// </summary>
        public float Distance { get; set; } = 0;
        /// <summary>
        /// End
        /// </summary>
        [Required]
        public Guid EndId { get; set; }
        /// <summary>
        /// Player
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
