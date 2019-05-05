using System;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Параметри для отримання статистики
    /// </summary>
    public class StatParam
    {
        /// <summary>
        /// Ідентиффікатор гравця
        /// </summary>
        public Guid? PlayerId { get; set; } = null;
        /// <summary>
        /// Дата з (включно)
        /// </summary>
        public DateTime? DateFrom { get; set; } = null;
        /// <summary>
        /// Дата по (вклюбчно)
        /// </summary>
        public DateTime? DateTo { get; set; } = null;
        /// <summary>
        /// Кваліфікація
        /// </summary>
        public int? CompetitionEvent { get; set; } = null;
        /// <summary>
        /// Сторона (0 - червоний, 1 - синій)
        /// </summary>
        public int? Side { get; set; } = null;
        /// <summary>
        /// Дистанція джека (0 - до 5 м, 1 - понад 5 м)
        /// </summary>
        public int? JackDistance { get; set; } = null;
        /// <summary>
        /// Етап на вибування
        /// </summary>
        public int? EliminationStage { get; set; } = null;
        /// <summary>
        /// Тип матчу (0 - вільні кидки тренування, 1 - офіційний матч, 2 - тренувальний матч)
        /// </summary>
        public int? MatchType { get; set; } = null;
        /// <summary>
        /// Ідентифікатор турніру
        /// </summary>
        public Guid? TournamentId { get; set; } = null;
        /// <summary>
        /// Ідентифікатоор типу турніру
        /// </summary>
        public Guid? TournamentTypeId { get; set; } = null;
        /// <summary>
        /// Ідентифікатор матчу (для статистики тільки за матчем)
        /// </summary>
        public Guid? MatchId { get; set; } = null;
        /// <summary>
        /// Ідентифікатор періоду (для статистики тільки за періодом гри)
        /// </summary>
        public Guid? StageId { get; set; } = null;
    }
}
