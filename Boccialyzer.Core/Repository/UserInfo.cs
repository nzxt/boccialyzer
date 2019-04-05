using System;
using System.Collections.Generic;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Інформація про користувача
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// Ім'я користувача
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// IP-адреса
        /// </summary>
        string IpAddress { get; set; }
        /// <summary>
        /// Ідентифікатор користувача
        /// </summary>
        Guid AppUserId { get; set; }
        /// <summary>
        /// Поточна локалізація
        /// </summary>
        string Locale { get; set; }
        /// <summary>
        /// Ролі користувача
        /// </summary>
        IList<string> Roles { get; set; }
        /// <summary>
        /// Чи адміністратор?
        /// </summary>
        bool IsAdmin { get; set; }
    }

    public class UserInfo : IUserInfo
    {
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public Guid AppUserId { get; set; }
        public string Locale { get; set; }
        public IList<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
    }
}
