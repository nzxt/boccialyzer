﻿using Boccialyzer.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boccialyzer.Domain.Entities
{
    /// <summary>
    /// Гравці
    /// </summary>
    public class Player : BaseEntity, IEntity
    {
        public Player()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Ім'я та прізвище
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Класифікація
        /// </summary>
        public PlayerClassification PlayerClassification { get; set; }
        /// <summary>
        /// Країна
        /// </summary>
        public Guid CountryId { get; set; }
    }
}
