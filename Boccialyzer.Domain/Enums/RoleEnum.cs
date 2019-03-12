using System;

namespace Boccialyzer.Domain.Enums
{
    /// <summary>
    /// Системні ролі
    /// </summary>
    public static class RoleEnum
    {
        /// <summary>
        /// Менеджер
        /// </summary>
        public static readonly Guid Manager = new Guid("5cff2bb4-1e3c-41b2-9bc2-3bb4cde53e66");
        /// <summary>
        ///Наш  Менеджер
        /// </summary>
        public static readonly Guid ManagerOwner = new Guid("f622470e-03c0-42d4-8b51-63e559ba3ab4");
        /// <summary>
        /// Респондент
        /// </summary>
        public static readonly Guid Respondent = new Guid("47c9d0a4-f2c4-4228-96f1-4dc645c1e58a");
        /// <summary>
        /// Адміністратор
        /// </summary>
        public static readonly Guid Administrator = new Guid("1caf7b48-8395-4b9d-909d-1b1a253bc208");
        /// <summary>
        /// Технолог
        /// </summary>
        public static readonly Guid Technologist = new Guid("ad6c2e51-9ede-4805-8ef3-27bc1a815d64");
        /// <summary>
        /// Експерт
        /// </summary>
        public static readonly Guid Expert = new Guid("407f5e8a-9be1-4e03-a036-5f9e31d8d55d");
        /// <summary>
        /// Суперюзер
        /// </summary>
        public static readonly Guid SuperUser = new Guid("7016011d-9773-4df9-b4d4-35d7567ea957");
        /// <summary>
        /// Наш експерт
        /// </summary>
        public static readonly Guid ExpertOwner = new Guid("440bc264-97f4-40e3-83b3-187d7edf8c5c");
    }
}
