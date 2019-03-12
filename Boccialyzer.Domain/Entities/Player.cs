using System;
using System.Collections.Generic;
using System.Text;
using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public PlayerClassification PlayerClassification { get; set; }
        public Guid NationalityId { get; set; }
    }
}
