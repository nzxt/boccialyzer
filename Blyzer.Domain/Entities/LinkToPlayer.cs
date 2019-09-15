using System;
using System.ComponentModel.DataAnnotations;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Relation to Players
    /// </summary>
    public abstract class LinkToPlayer : BaseEntity
    {
        /// <summary>
        /// BIB
        /// </summary>
        public int Bib { get; set; }
        /// <summary>
        /// Box
        /// </summary>
        [Required]
        public int Box { get; set; }
        /// <summary>
        /// Player ID
        /// </summary>
        [Required]
        public Guid PlayerId { get; set; }
    }
}
