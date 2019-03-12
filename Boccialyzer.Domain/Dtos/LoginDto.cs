using System;
using System.Collections.Generic;
using System.Text;

namespace Boccialyzer.Domain.Dtos
{
    /// <summary>
    /// Модель логіну
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Ім'я користувача
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Пароль користувача
        /// </summary>
        public string Password { get; set; }
    }
}
