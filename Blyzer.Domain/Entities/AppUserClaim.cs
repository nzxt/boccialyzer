using Microsoft.AspNetCore.Identity;
using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Application`s User Claim
    /// </summary>
    public class AppUserClaim : IdentityUserClaim<Guid>
    {
        /// <summary>
        /// App User
        /// </summary>
        public virtual AppUser User { get; set; }
    }
}
