using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Domain.Entities
{
    public class Match : BaseEntity,IEntity
    {
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

        public Guid? Box1PlayerId  { get; set; }
        public int Box1PlayerBib  { get; set; }
        public Guid? Box2PlayerId  { get; set; }
        public int Box2PlayerBib  { get; set; }
        [Required]
        public Guid Box3PlayerId { get; set; }
        public int Box3PlayerBib  { get; set; }
        [Required]
        public Guid Box4PlayerId { get; set; }
        public int Box4PlayerBib  { get; set; }
        public Guid? Box5PlayerId  { get; set; }
        public int Box5PlayerBib  { get; set; }
        public Guid? Box6PlayerId  { get; set; }
        public int Box6PlayerBib { get; set; }




        /// <summary>
        /// М'ячі
        /// </summary>
        public virtual ICollection<MatchBall> MatchBalls { get; set; } = new Collection<MatchBall>();
    }
}
