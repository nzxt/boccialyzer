using Blyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Match
    /// </summary>
    public class Match : BaseEntity, IBaseEntity
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Дата та час проведення
        /// </summary>
        //[Required]
        public DateTime DateTimeStamp { get; set; }
        /// <summary>
        /// Тип матчу
        /// </summary>
        [Index]
        public MatchType MatchType { get; set; } = MatchType.None;
        /// <summary>
        /// Оцінка матчу
        /// </summary>
        [Index]
        public int Rate { get; set; } = 0;
        /// <summary>
        /// Чи надавати публічний доступ
        /// </summary>
        public bool IsPublic { get; set; } = false;
        /// <summary>
        /// Competition Event
        /// </summary>
        [Index]
        public CompetitionEvent CompetitionEvent { get; set; } = CompetitionEvent.None;
        /// <summary>
        /// Етап пулу
        /// </summary>
        [Index]
        public string PoolStage { get; set; }
        /// <summary>
        /// Етап на вибування
        /// </summary>
        [Index]
        public EliminationStage EliminationStage { get; set; }
        /// <summary>
        /// Рахунок червоних
        /// </summary>
        public int ScoreRed { get; set; } = 0;
        /// <summary>
        /// Рахунок синіх
        /// </summary>
        public int ScoreBlue { get; set; } = 0;
        /// <summary>
        /// Середня якість кидків червоних
        /// </summary>
        public int AvgPointRed { get; set; } = 0;
        /// <summary>
        /// Середня якість кидків синіх
        /// </summary>
        public int AvgPointBlue { get; set; } = 0;
        /// <summary>
        /// Ідентифікатор прапору для червоних
        /// </summary>
        public string FlagRed { get; set; }
        /// <summary>
        /// Ідентифікатор прапору для синіх
        /// </summary>
        public string FlagBlue { get; set; }
        /// <summary>
        /// Турнір
        /// </summary>
        public Guid? TournamentId { get; set; }

        /// <summary>
        /// Зв'язок гравців з матчами/тренуваннями
        /// </summary>
        public virtual ICollection<MatchToPlayer> MatchToPlayers { get; set; } = new Collection<MatchToPlayer>();
        /// <summary>
        /// Періоди гри
        /// </summary>
        public virtual ICollection<End> Ends { get; set; } = new Collection<End>();
    }
}
