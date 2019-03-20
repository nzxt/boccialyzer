using System;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Модель для сворення нового користувача (респондента)
    /// </summary>
    public class NewUserModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NewUserModel()
        {
            InvitationId = default(Guid);
        }
        /// <summary>
        /// Ім'я користувача
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Ідентифікатор запрошення
        /// </summary>
        public Guid InvitationId { get; set; }

    }
}
