using System;
using System.Collections.Generic;
using System.Text;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Турнір
    /// </summary>
    public class Tournament : BaseEntity
    {
        /// <summary>
        /// Назва
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тип турнирів
        /// </summary>
        public Guid TournamentTypeId { get; set; }
    }
}
