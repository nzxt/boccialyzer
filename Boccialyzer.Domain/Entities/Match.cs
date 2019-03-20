using Boccialyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Матч
    /// </summary>
    public class Match : BaseEntity, IEntity
    {
        /// <summary>
        /// Match constructor
        /// </summary>
        public Match()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Дата та час проведення
        /// </summary>
        [Required]
        public DateTime DateTimeStamp { get; set; }
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
        /// Користувач системи
        /// </summary>
        [Required]
        public Guid AppUserId { get; set; }

        /// <summary>
        /// Турнір
        /// </summary>
        public Guid? TournamentId { get; set; }

        /// <summary>
        /// Зв'язок гравців з матчами/тренуваннями
        /// </summary>
        public virtual ICollection<MatchToPlayer> MatchToPlayers { get; set; } = new Collection<MatchToPlayer>();
    }
}
