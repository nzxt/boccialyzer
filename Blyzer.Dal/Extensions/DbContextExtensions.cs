using Blyzer.Dal.Context;
using Blyzer.Domain.Entities;
using Blyzer.Domain.Enums;
using Blyzer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blyzer.Dal.Extensions
{
    /// <summary>
    /// DbContext Extensions
    /// </summary>
    public static class DbContextExtensions
    {
        #region # EnsureSeededAsync()

        /// <summary>
        /// Seed requared data
        /// </summary>
        /// <param name="dbContext">DB context</param>
        /// <param name="roleManager">RoleManager service</param>
        /// <param name="userManager">UserManager service</param>
        /// <returns></returns>
        public static async Task EnsureSeededAsync(this AppDbContext dbContext, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            try
            {
                await dbContext.Seed<Country>("countries.json");
                await dbContext.Seed<Player>("players.json");
                await dbContext.Seed<TournamentType>("tournamenttypes.json");
                await dbContext.SeedRole(roleManager, "role.json");
                await dbContext.SeedUser(roleManager, userManager);
                //StoredFunctionData.Seed(dbContext);
                //TriggerData.Seed(dbContext);
                //StoredViewData.Seed(dbContext);
                Log.Information("{Info}", "Seed data.................. OK");
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }
        }

        #endregion
        #region # EnsureSeededDevAsync()

        /// <summary>
        /// Seed developer data
        /// </summary>
        /// <param name="dbContext">DB context</param>
        /// <returns></returns>
        public static async Task EnsureSeededDevAsync(this AppDbContext dbContext)
        {
            try
            {
                await dbContext.Seed<Match>("match.json");
                await dbContext.Seed<MatchToPlayer>("matchtoplayer.json");
                await dbContext.Seed<End>("end.json");
                await dbContext.Seed<EndToPlayer>("endtoplayer.json");
                await dbContext.Seed<Ball>("ball.json");
                Log.Information("{Info}", "Seed dev data.............. OK");
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }
        }

        #endregion
        #region # Task ApplyMigrationsAsync()

        /// <summary>
        /// Apply Migrations
        /// </summary>
        /// <param name="dbContext">DB Context</param>
        /// <returns></returns>
        public static async Task ApplyMigrationsAsync(this AppDbContext dbContext)
        {
            try
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    await dbContext.Database.MigrateAsync();
                }
                Log.Information("{Info}", "Apply Migrations........... OK");
            }
            catch (Exception ex)
            {
                Log.Error("{MigrationError}", ex.Message);
            }
        }

        #endregion


        #region # Seed(string filename)

        /// <summary>
        /// Seed data
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <param name="dbContext">Application DB context</param>
        /// <param name="filename">JSON filename</param>
        /// <returns></returns>
        public static async Task Seed<TEntity>(this AppDbContext dbContext, string filename) where TEntity : class
        {
            try
            {
                if (dbContext.Set<TEntity>().IgnoreQueryFilters().Any()) return;

                var entities = GetEntitiesFromFile<TEntity>(filename);

                await dbContext.Set<TEntity>().AddRangeAsync(entities);
                var result = await dbContext.SaveChangesAsync();

                if (result == 0)
                    Log.Error("{SeedError}", "Seed data: something went wrong.");
                return;
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }
        }


        #endregion
        #region # SeedRole(RoleManager<AppRole> roleManager, string filename)

        /// <summary>
        /// Seed application roles
        /// </summary>
        /// <param name="dbContext">Application DB context</param>
        /// <param name="roleManager">RoleManager service</param>
        /// <param name="filename">JSON filename</param>
        /// <returns></returns>
        public static async Task SeedRole(this AppDbContext dbContext, RoleManager<AppRole> roleManager, string filename)
        {
            try
            {
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", filename);

                if (!File.Exists(jsonPath)) throw new Exception($"{filename} is not found");
                var entities = JsonConvert.DeserializeObject<List<AppRole>>(File.ReadAllText(jsonPath));

                foreach (var item in entities)
                {
                    if (!await roleManager.RoleExistsAsync(item.Name))
                    {
                        var result = await roleManager.CreateAsync(item);
                        if (!result.Succeeded)
                        {
                            Log.Error("{SeedError}", "Seed roles: Something went wrong.");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }

        }

        #endregion
        #region # SeedUser(UserManager<AppUser> userManager, string filename)

        /// <summary>
        /// Seed application user
        /// </summary>
        /// <param name="dbContext">Application DB context</param>
        /// <param name="roleManager">RoleManager service</param>
        /// <param name="userManager">UserManager service</param>
        /// <returns></returns>
        public static async Task SeedUser(this AppDbContext dbContext, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            try
            {
                AppUserExtended[] users =
                {
                    new AppUserExtended
                    {
                        Id = new Guid("8f666c33-f0c1-41a9-bc70-8628bfe521b5"),
                        UserName = "admin",
                        FirstName = "Administrator",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role = RoleEnum.Administrator
                    },
                    new AppUserExtended
                    {
                        Id = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                        UserName = "user1",
                        FirstName = "User1",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role=RoleEnum.User
                    },
                    new AppUserExtended
                    {
                        Id = new Guid("31465951-a55d-49b3-915d-17f01fb3c73b"),
                        UserName = "user2",
                        FirstName = "User2",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role=RoleEnum.User
                    },
                    new AppUserExtended
                    {
                        Id = new Guid("83de8388-cb9a-44dc-bfec-a93c78845b8c"),
                        UserName = "manager",
                        FirstName = "Manager",
                        LastName = "LastName",
                        Gender = Gender.Male,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role=RoleEnum.Manager
                    }
                };

                string password = "qwerty";

                foreach (var item in users)
                {
                    if (await userManager.FindByNameAsync(item.UserName) == null)
                    {
                        var result = await userManager.CreateAsync(item, password);
                        if (!result.Succeeded)
                            throw new Exception("Seed users: Create user error.");
                        var role = await roleManager.FindByIdAsync(item.Role.ToString());
                        if (role != null)
                        {
                            var addResult = await userManager.AddToRoleAsync(item, role.Name);
                            if (!addResult.Succeeded)
                                throw new Exception("Seed users: Adding to role error.");
                        }
                        else
                            throw new Exception("Seed users: Role is not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }

        }

        #endregion
        #region ## GetEntitiesFromFile<TEntity>(string filename)

        /// <summary>
        /// Read data from JSON-file and deserialize to entity
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <param name="filename">JSON filename</param>
        /// <returns></returns>
        private static List<TEntity> GetEntitiesFromFile<TEntity>(string filename) where TEntity : class
        {
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", filename);

            if (!File.Exists(jsonPath)) throw new Exception($"{filename} is not found");
            return JsonConvert.DeserializeObject<List<TEntity>>(File.ReadAllText(jsonPath));
        }

        #endregion
    }
}
