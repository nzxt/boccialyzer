using System;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// М'ячі тренування
    /// </summary>
    public class TrainingBall : Ball
    {
        /// <summary>
        /// Ідентифікатор тренування
        /// </summary>
        public Guid TrainingId { get; set; }
    }
}
