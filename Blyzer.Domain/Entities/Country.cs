using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Країни
    /// </summary>
    public class Country : BaseEntity, IBaseEntity
    {
        #region Country constructor

        /// <summary>
        /// Country constructor
        /// </summary>
        public Country()
        {
            Id = Guid.NewGuid();
        }

        #endregion
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Назва
        /// </summary>
        [Required]
        [Index]
        public string Name { get; set; }
        /// <summary>
        /// Код країни
        /// </summary>
        [Index]
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
        /// Користувачі
        /// </summary>
        public ICollection<AppUser> AppUsers { get; set; } = new Collection<AppUser>();
    }
}
