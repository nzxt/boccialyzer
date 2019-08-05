using Microsoft.AspNetCore.Identity;
using System;

namespace Blyzer.Domain.Entities
{
    /// <summary>
    /// Application`s User Login
    /// </summary>
    public class AppUserLogin : IdentityUserLogin<Guid>
    {
        /// <summary>
        /// Application`s User
        /// </summary>
        public virtual AppUser User { get; set; }
    }
}
