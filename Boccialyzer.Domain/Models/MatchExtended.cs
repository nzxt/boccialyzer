using Boccialyzer.Domain.Enums;
using System;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Матч (розширено)
    /// </summary>
    public class MatchExtended
    {
        /// <summary>
        /// Назва турніру
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// Тип турниру
        /// </summary>
        public Guid TournamentTypeId { get; set; }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Користувач системи
        /// </summary>
        public Guid AppUserId { get; set; }
        /// <summary>
        /// Дата та час проведення
        /// </summary>
        public DateTime DateTimeStamp { get; set; }
        /// <summary>
        /// Тип матчу
        /// </summary>
        public MatchType MatchType { get; set; }
        /// <summary>
        /// Червоні гравці
        /// </summary>
        public string PlayersRed { get; set; }
        /// <summary>
        /// Сині гравці
        /// </summary>
        public string PlayersBlue { get; set; }
        /// <summary>
        /// Рахунок червоних
        /// </summary>
        public int ScoreRed { get; set; }
        /// <summary>
        /// Рахунок синіх
        /// </summary>
        public int ScoreBlue { get; set; }
        /// <summary>
        /// Competition Event
        /// </summary>
        public CompetitionEvent CompetitionEvent { get; set; }
        /// <summary>
        /// Етап пулу
        /// </summary>
        public PoolStage PoolStage { get; set; }
        /// <summary>
        /// Етап на вибування
        /// </summary>
        public EliminationStage EliminationStage { get; set; }
        /// <summary>
        /// Ідентифікатор прапору для червоних
        /// </summary>
        public string FlagRed { get; set; }
        /// <summary>
        /// Ідентифікатор прапору для синіх
        /// </summary>
        public string FlagBlue { get; set; }
        /// <summary>
        /// Тренування
        /// </summary>
        public Guid? TrainingId { get; set; }
        /// <summary>
        /// Турнір
        /// </summary>
        public Guid? TournamentId { get; set; }
    }
}