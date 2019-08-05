using System;

namespace Blyzer.Domain.Enums
{
    /// <summary>
    /// System roles enum
    /// </summary>
    public static class RoleEnum
    {
        /// <summary>
        /// Manager
        /// </summary>
        public static readonly Guid Manager = new Guid("5cff2bb4-1e3c-41b2-9bc2-3bb4cde53e66");
        /// <summary>
        /// Administrator
        /// </summary>
        public static readonly Guid Administrator = new Guid("8f666c33-f0c1-41a9-bc70-8628bfe521b5");
        /// <summary>
        /// User
        /// </summary>
        public static readonly Guid User = new Guid("47c9d0a4-f2c4-4228-96f1-4dc645c1e58a");
    }
}
