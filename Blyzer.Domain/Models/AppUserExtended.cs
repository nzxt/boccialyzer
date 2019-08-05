using System;
using Blyzer.Domain.Entities;
using Blyzer.Domain.Enums;

namespace Blyzer.Domain.Models
{
    public class AppUserExtended:AppUser
    {
        public Guid Role { get; set; } = RoleEnum.User;
    }
}
