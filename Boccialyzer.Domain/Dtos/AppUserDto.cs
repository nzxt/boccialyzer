using Boccialyzer.Domain.Entities;
using System.Collections;

namespace Boccialyzer.Domain.Dtos
{
    /// <summary>
    /// Користувач
    /// </summary>
    public class AppUserDto : AppUser
    {
        /// <summary>
        /// Ролі користувача
        /// </summary>
        public IEnumerable Roles { get; set; }
    }
}
