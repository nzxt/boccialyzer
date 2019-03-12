using System;

namespace Boccialyzer.Core.Repository
{
    public interface IUserInfo
    {
        string UserName { get; set; }
        string IpAddress { get; set; }
        Guid AppUserId { get; set; }
        string Locale { get; set; }
        string RoleName { get; set; }
        Guid RoleId { get; set; }
    }

    public class UserInfo : IUserInfo
    {
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public Guid AppUserId { get; set; }
        public string Locale { get; set; }
        public string RoleName { get; set; }
        public Guid RoleId { get; set; }
    }
}
