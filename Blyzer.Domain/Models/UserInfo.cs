using System;

namespace Blyzer.Domain.Models
{
    /// <summary>
    /// Current user infos
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// UserName
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// IP address
        /// </summary>
        string IpAddress { get; set; }
        /// <summary>
        /// AppUserId
        /// </summary>
        Guid AppUserId { get; set; }
        /// <summary>
        /// User`s locale
        /// </summary>
        string Locale { get; set; }
        /// <summary>
        /// User`s role
        /// </summary>
        string Role { get; set; }
        /// <summary>
        /// Is user admin?
        /// </summary>
        bool IsAdmin { get; set; }
    }

    /// <summary>
    /// Current user infos
    /// </summary>
    public class UserInfo : IUserInfo
    {
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// IP address
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// AppUserId
        /// </summary>
        public Guid AppUserId { get; set; }
        /// <summary>
        /// User`s locale
        /// </summary>
        public string Locale { get; set; }
        /// <summary>
        /// User`s role
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Is user admin?
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
