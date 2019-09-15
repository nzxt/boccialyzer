using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Country
    /// </summary>
    public class Country : BaseEntity
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Alpha2
        /// </summary>
        public string Alpha2 { get; set; }
        /// <summary>
        /// Alpha3
        /// </summary>
        public string Alpha3 { get; set; }
        /// <summary>
        /// Users
        /// </summary>
        [Obsolete]
        public virtual ICollection<AppUser> AppUsers { get; set; } = new Collection<AppUser>();
    }
}
