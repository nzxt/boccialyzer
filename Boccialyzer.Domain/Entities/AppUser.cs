using Microsoft.AspNetCore.Identity;
using Boccialyzer.Domain.Attributes;
using Boccialyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Системні користувачі
    /// </summary>
    public class AppUser : IdentityUser<Guid>
    {
        #region AppUser constructor

        /// <summary>
        /// AppUser constructor
        /// </summary>
        public AppUser()
        {
            CreatedOn = DateTime.UtcNow;
            CreatedBy = default(Guid);
        }

        #endregion

        #region # service fields

        /// <summary>
        /// Дата і час заведення
        /// </summary>
        [Obsolete]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Дата та час редагування
        /// </summary>
        [Obsolete]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// Користувач системи, що створив запис
        /// </summary>
        [Obsolete]
        public Guid CreatedBy { get; set; }
        /// <summary>
        /// Користувач системи, що модифікував запис
        /// </summary>
        [Obsolete]
        public Guid? UpdatedBy { get; set; }

        #endregion

        /// <summary>
        /// Національність
        /// </summary>
        public Guid? NationalityId { get; set; }
        /// <summary>
        /// Національність
        /// </summary>
        [Obsolete]
        public Nationality Nationality { get; set; }
        /// <summary>
        /// Ім'я
        /// </summary>
        [MaxLength(50)]
        public string FirstName { get; set; }
        /// <summary>
        /// Прізвище
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Дата народження
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Стать
        /// </summary>
        public Gender Gender { get; set; }



        #region # override prop

        /// <summary>
        /// NormalizedUserName
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override string NormalizedUserName
        {
            get => base.NormalizedUserName;
            set => base.NormalizedUserName = value;
        }
        /// <summary>
        /// NormalizedEmail
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override string NormalizedEmail
        {
            get => base.NormalizedEmail;
            set => base.NormalizedEmail = value;
        }
        /// <summary>
        /// PasswordHash
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override string PasswordHash
        {
            get => base.PasswordHash;
            set => base.PasswordHash = value;
        }
        /// <summary>
        /// SecurityStamp
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override string SecurityStamp
        {
            get => base.SecurityStamp;
            set => base.SecurityStamp = value;
        }
        /// <summary>
        /// ConcurrencyStamp
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override string ConcurrencyStamp
        {
            get => base.ConcurrencyStamp;
            set => base.ConcurrencyStamp = value;
        }
        /// <summary>
        /// PhoneNumber
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override string PhoneNumber
        {
            get => base.PhoneNumber;
            set => base.PhoneNumber = value;
        }
        /// <summary>
        /// Email
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override string Email
        {
            get => base.Email;
            set => base.Email = value;
        }
        /// <summary>
        /// EmailConfirmed
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override bool EmailConfirmed
        {
            get => base.TwoFactorEnabled;
            set => base.TwoFactorEnabled = value;
        }
        /// <summary>
        /// PhoneNumberConfirmed
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override bool PhoneNumberConfirmed
        {
            get => base.PhoneNumberConfirmed;
            set => base.PhoneNumberConfirmed = value;
        }
        /// <summary>
        /// TwoFactorEnabled
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override bool TwoFactorEnabled
        {
            get => base.TwoFactorEnabled;
            set => base.TwoFactorEnabled = value;
        }
        /// <summary>
        /// AccessFailedCount
        /// </summary>
        [AuditIgnore]
        [Obsolete]
        public override int AccessFailedCount
        {
            get => base.AccessFailedCount;
            set => base.AccessFailedCount = value;
        }

        #endregion
    }
}