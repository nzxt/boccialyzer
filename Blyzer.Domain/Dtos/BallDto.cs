using System;

namespace Blyzer.Domain.Dtos
{
    /// <summary>
    /// Ball DTO
    /// </summary>
    public class BallDto
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
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
    }
}
