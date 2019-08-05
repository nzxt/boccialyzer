using Microsoft.AspNetCore.Identity;
using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Application`s RoleId Claim
    /// </summary>
    public class AppRoleClaim : IdentityRoleClaim<Guid>
    {
        /// <summary>
        /// Application`s RoleId
        /// </summary>
        public virtual AppRole Role { get; set; }
    }
}
