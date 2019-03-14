using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Турнір
    /// </summary>
    public class Tournament : BaseEntity, IEntity
    {
        public Tournament()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Назва
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Тип турнирів
        /// </summary>
        [Required]
        public Guid TournamentTypeId { get; set; }
        /// <summary>
        /// Дата початку
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// Дата завершення
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime DateTo { get; set; }
        /// <summary>
        /// Користувач системи
        /// </summary>
        [Required]
        public Guid AppUserId { get; set; }
        /// <summary>
        /// Матчі
        /// </summary>
        public virtual ICollection<Match> Matches { get; set; } = new Collection<Match>();
    }
}
