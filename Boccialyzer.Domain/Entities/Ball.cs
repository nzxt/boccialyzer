using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    public class Ball : BaseEntity, IEntity
    {
        public Ball()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public bool IsPenalty { get; set; }
        public bool IsDeadBall { get; set; }
    }
}
