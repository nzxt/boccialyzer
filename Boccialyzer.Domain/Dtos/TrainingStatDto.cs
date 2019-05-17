using Boccialyzer.Domain.Enums;
using System;

namespace Boccialyzer.Domain.Dtos
{
    /// <summary>
    /// Статистика тренування
    /// </summary>
    public class TrainingStatDto
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Дата та час тренування
        /// </summary>
        public DateTime DateTimeStamp { get; set; }
        /// <summary>
        /// Тип тренування
        /// </summary>
        public TrainingType TrainingType { get; set; }
    }
}
