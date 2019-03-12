using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Boccialyzer.Domain;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Громадянство
    /// </summary>
    public partial class Nationality : BaseEntity, IEntity
    {
        #region Nationality constructor

        /// <summary>
        /// Nationality constructor
        /// </summary>
        public Nationality()
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
        /// За замовчуванням
        /// </summary>
        [DefaultValue(false)]
        public bool IsDefault { get; set; }
        /// <summary>
        /// Назва
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Код країни
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
        /// Користувачі
        /// </summary>
        public ICollection<AppUser> AppUsers { get; set; } = new Collection<AppUser>();
    }
}
