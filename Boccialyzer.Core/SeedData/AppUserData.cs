using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Boccialyzer.Domain.Entities;
using System;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Core.SeedData
{
    /// <summary>
    /// Додавання користувачів
    /// </summary>
    public static class AppUserData
    {
        #region # Task Seed(IServiceProvider serviceProvider, bool isDevelopment)

        public static async Task Seed(IServiceProvider serviceProvider, bool isDevelopment)
        {

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();
            dbContext.LoggingDisable = true;

            if (!dbContext.Users.Any())
            {
                #region # admin

                string userName = "admin";
                var userExist = await userManager.FindByNameAsync(userName);
                if (userExist == null)
                {
                    string password = "qwerty";
                    var user = new AppUser
                    {
                        Id = new Guid("8f666c33-f0c1-41a9-bc70-8628bfe521b5"),
                        UserName = userName,
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync(RoleEnum.Administrator.ToString());
                        if (role != null) await userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                #endregion
                #region # respondent1

                userName = "respondent1";
                userExist = await userManager.FindByNameAsync(userName);
                if (userExist == null)
                {
                    string password = "qwerty";
                    var user = new AppUser
                    {
                        Id = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                        UserName = userName,
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync(RoleEnum.Respondent.ToString());
                        if (role != null) await userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                #endregion
                #region # respondent2

                userName = "respondent2";
                userExist = await userManager.FindByNameAsync(userName);
                if (userExist == null)
                {
                    string password = "qwerty";
                    var user = new AppUser
                    {
                        Id = new Guid("31465951-a55d-49b3-915d-17f01fb3c73b"),
                        UserName = userName,
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync(RoleEnum.Respondent.ToString());
                        if (role != null) await userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                #endregion
                #region # technologist

                userName = "technologist";
                userExist = await userManager.FindByNameAsync(userName);
                if (userExist == null)
                {
                    string password = "qwerty";
                    var user = new AppUser
                    {
                        Id = new Guid("2d2b0c24-cf72-4879-ab51-90214829427e"),
                        UserName = userName,
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync(RoleEnum.Technologist.ToString());
                        if (role != null) await userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                #endregion
                #region # expert

                userName = "expert";
                userExist = await userManager.FindByNameAsync(userName);
                if (userExist == null)
                {
                    string password = "qwerty";
                    var user = new AppUser
                    {
                        Id = new Guid("853a3c4a-7409-4fbb-a6f7-cebaa30d7d23"),
                        UserName = userName,
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync(RoleEnum.Expert.ToString());
                        if (role != null) await userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                #endregion
                #region # SuperUser

                userName = "SuperUser";
                userExist = await userManager.FindByNameAsync(userName);
                if (userExist == null)
                {
                    string password = "qwerty";
                    var user = new AppUser
                    {
                        Id = new Guid("1def0cb0-96c2-4130-aa45-fb5f9a73e745"),
                        UserName = userName,
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync(RoleEnum.SuperUser.ToString());
                        if (role != null) await userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                #endregion
                #region # manager

                userName = "manager";
                userExist = await userManager.FindByNameAsync(userName);
                if (userExist == null)
                {
                    string password = "qwerty";
                    var user = new AppUser
                    {
                        Id = new Guid("83de8388-cb9a-44dc-bfec-a93c78845b8c"),
                        UserName = userName,
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync(RoleEnum.Manager.ToString());
                        if (role != null) await userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                #endregion
            }

            dbContext.LoggingDisable = false;
        }

        #endregion
    }
}
