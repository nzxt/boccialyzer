using System;
using System.ComponentModel.DataAnnotations;
using Boccialyzer.Domain;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Application configuration entity
    /// </summary>
    public class Configuration : BaseEntity, IEntity
    {
        public Configuration()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Setting Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
    }
}
