using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Boccialyzer.Domain.Entities;
using System;
using System.Threading.Tasks;
using Boccialyzer.Core.Context;

namespace Boccialyzer.Core.SeedData
{
    /// <summary>
    /// Додавання ролей
    /// </summary>
    public static class AppRoleData
    {
        #region # AppRoles

        private static readonly AppRole[] AppRoles =
        {
            new AppRole
            {
                Id = new Guid("5cff2bb4-1e3c-41b2-9bc2-3bb4cde53e66"),
                Name = "Manager",
                Caption = "Менеджер",
                IsDeleted = false,
                IsDefault = false,
                IsAdministrator = false,
                IsExpert = false,
                IsManager = true,
                IsOwner = false,
                IsRespondent = false,
                IsSuperUser = false,
                IsSystem = true
            },
            new AppRole
            {
                Id = new Guid("f622470e-03c0-42d4-8b51-63e559ba3ab4"),
                Name = "ManagerOwner",
                Caption = "Наш Менеджер",
                IsDeleted = false,
                IsDefault = false,
                IsAdministrator = false,
                IsExpert = false,
                IsManager = true,
                IsOwner = true,
                IsRespondent = false,
                IsSuperUser = false,
                IsSystem = true
            },
            new AppRole
            {
                Id = new Guid("47c9d0a4-f2c4-4228-96f1-4dc645c1e58a"),
                Name = "Respondent",
                Caption = "Респондент",
                IsDeleted = false,
                IsDefault = false,
                IsAdministrator = true,
                IsExpert = false,
                IsManager = false,
                IsOwner = false,
                IsRespondent = true,
                IsSuperUser = false,
                IsSystem = true
            },
            new AppRole
            {
                Id = new Guid("1caf7b48-8395-4b9d-909d-1b1a253bc208"),
                Name = "Administrator",
                Caption = "Адміністратор",
                IsDeleted = false,
                IsDefault = false,
                IsAdministrator = true,
                IsExpert = false,
                IsManager = false,
                IsOwner = true,
                IsRespondent = false,
                IsSuperUser = false,
                IsSystem = true
            },
            new AppRole
            {
                Id = new Guid("ad6c2e51-9ede-4805-8ef3-27bc1a815d64"),
                Name = "Technologist",
                Caption = "Технолог",
                IsDeleted = false,
                IsDefault = false,
                IsAdministrator = false,
                IsExpert = false,
                IsManager = false,
                IsOwner = false,
                IsRespondent = false,
                IsSuperUser = true,
                IsSystem = true
            },
            new AppRole
            {
                Id = new Guid("407f5e8a-9be1-4e03-a036-5f9e31d8d55d"),
                Name = "Expert",
                Caption = "Експерт",
                IsDeleted = false,
                IsDefault = false,
                IsAdministrator = false,
                IsExpert = true,
                IsManager = false,
                IsOwner = false,
                IsRespondent = false,
                IsSuperUser = false,
                IsSystem = true
            },
            new AppRole
            {
                Id = new Guid("7016011d-9773-4df9-b4d4-35d7567ea957"),
                Name = "SuperUser",
                Caption = "Суперюзер",
                IsDeleted = false,
                IsDefault = false,
                IsAdministrator = false,
                IsExpert = false,
                IsManager = false,
                IsOwner = true,
                IsRespondent = false,
                IsSuperUser = true,
                IsSystem = true
            },
            new AppRole
            {
                Id = new Guid("440bc264-97f4-40e3-83b3-187d7edf8c5c"),
                Name = "ExpertOwner",
                Caption = "Наш експерт",
                IsDeleted = false,
                IsDefault = false,
                IsAdministrator = false,
                IsExpert = true,
                IsManager = false,
                IsOwner = true,
                IsRespondent = false,
                IsSuperUser = false,
                IsSystem = true
            }
        };

        #endregion
        #region # Task Seed(IServiceProvider serviceProvider)

        /// <summary>
        /// Додавання ролей
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.LoggingDisable = true;

            foreach (var item in AppRoles)
            {
                if (!await roleManager.RoleExistsAsync(item.Name))
                {
                    await roleManager.CreateAsync(item);
                }
            }
            dbContext.LoggingDisable = false;
        }

        #endregion
    }
}
