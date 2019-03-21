using Boccialyzer.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Модель для сворення нового користувача
    /// </summary>
    public class NewUserModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NewUserModel()
        {
            Gender = Gender.Undefined;
            RoleId = default(Guid);
            CountryId = default(Guid);
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
        /// Національність (optional)
        /// </summary>
        public Guid? CountryId { get; set; }
        /// <summary>
        /// Ім'я
        /// </summary>
        [MaxLength(50)]
        public string FirstName { get; set; }
        /// <summary>
        /// Прізвище
        /// </summary>
        [MaxLength(50)]
        public string LastName { get; set; }
        /// <summary>
        /// Дата народження
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Стать
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// Роль користувача (optional)
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
