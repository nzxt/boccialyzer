namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Статистика - результат
    /// </summary>
    public class StatResult
    {
        /// <summary>
        /// Тип кидка
        /// </summary>
        public int ShotType { get; set; }
        /// <summary>
        /// Середня оцінка
        /// </summary>
        public decimal Ratings { get; set; }
        /// <summary>
        /// Кількість кидків за типом
        /// </summary>
        public int BallCount { get; set; }
        /// <summary>
        /// Загальна кількість кидків
        /// </summary>
        public int TotalBallCount { get; set; }
        /// <summary>
        /// Відсоток від загальної кількості
        /// </summary>
        public decimal Percentage { get; set; }
    }
}
