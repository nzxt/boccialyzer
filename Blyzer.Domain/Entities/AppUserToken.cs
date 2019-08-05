using System;
using Microsoft.AspNetCore.Identity;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Application`s User Token
    /// </summary>
    public class AppUserToken : IdentityUserToken<Guid>
    {
        /// <summary>
        /// Application`s User
        /// </summary>
        public virtual AppUser User { get; set; }
    }
}
