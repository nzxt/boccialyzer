﻿using Boccialyzer.Domain.Enums;
using System;

namespace Boccialyzer.Domain.Dtos
{
    /// <summary>
    /// Тренування
    /// </summary>
    public class TrainingDto
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
        /// <summary>
        /// Середня оцінка кидків
        /// </summary>
        public double AvgBallRating { get; set; }
        /// <summary>
        /// Оцінка тренування
        /// </summary>
        public int Rate { get; set; }
    }
}
