using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Tournament
    /// </summary>
    public class Tournament : BaseEntity, IBaseEntity
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Tournament Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Tournament type
        /// </summary>
        [Required]
        public Guid TournamentTypeId { get; set; }
        /// <summary>
        /// Date From
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime DateFrom { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Date To
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime DateTo { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Matches of tournament
        /// </summary>
        [Obsolete]
        public virtual ICollection<Match> Matches { get; set; } = new Collection<Match>();
    }
}
