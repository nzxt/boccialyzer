﻿using System;
using System.Collections.Generic;

namespace Boccialyzer.Domain.Dtos
{
    /// <summary>
    /// Інформація користувача
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Ім'я користувача
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Користувач системи
        /// </summary>
        public Guid AppUserId { get; set; }
        /// <summary>
        /// Ролі користувача
        /// </summary>
        public string Role { get; set; }
    }
}
