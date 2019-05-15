using System;
using System.Collections.Generic;
using System.Text;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Статистика тренування (free balls)
    /// </summary>
    public class StatResultTraining
    {
        /// <summary>
        /// Тип кидка
        /// </summary>
        public int ShotType { get; set; }
        /// <summary>
        /// Дистанція
        /// </summary>
        public int Distance { get; set; }
        /// <summary>
        /// Середня оцінка
        /// </summary>
        public decimal AvgRating { get; set; }
        /// <summary>
        /// Кількість м'ячів
        /// </summary>
        public int Count { get; set; }
    }
}
