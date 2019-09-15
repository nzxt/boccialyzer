using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Tournament Type
    /// </summary>
    public class TournamentType : BaseEntity
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Abbreviation
        /// </summary>
        [Required]
        public string Abbreviation { get; set; }
        /// <summary>
        /// Is it an official BISFed tournament?
        /// </summary>
        public bool IsBisFed { get; set; } = true;
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Tournaments
        /// </summary>
        public ICollection<Tournament> Tournaments { get; set; } = new Collection<Tournament>();
    }
}
