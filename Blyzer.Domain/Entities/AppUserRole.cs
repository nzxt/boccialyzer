using Microsoft.AspNetCore.Identity;
using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Application User RoleId
    /// </summary>
    public class AppUserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// User
        /// </summary>
        public virtual AppUser User { get; set; }
        /// <summary>
        /// RoleId
        /// </summary>
        public virtual AppRole Role { get; set; }
    }
}
