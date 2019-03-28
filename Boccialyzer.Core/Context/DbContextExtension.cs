using Boccialyzer.Core.SeedData;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boccialyzer.Core.Context
{
    /// <summary>
    /// Додавання данних
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// Додавання данних
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="isDevelopment">Режим роботи</param>
        /// <returns></returns>
        public static async Task EnsureSeededAsync(this ApplicationDbContext dbContext, bool isDevelopment)
        {
            dbContext.LoggingDisable = true;

            try
            {
                if (!dbContext.TournamentTypes.IgnoreQueryFilters().Any()) await TournamentTypeData.Seed(dbContext);
                if (!dbContext.Countries.IgnoreQueryFilters().Any()) await CountryData.Seed(dbContext);
                if (!dbContext.Configurations.Any()) await ConfigurationData.Seed(dbContext);
                if (!dbContext.Players.Any()) await PlayerData.Seed(dbContext);

                if (isDevelopment)
                {
                    if (!dbContext.Trainings.IgnoreQueryFilters().Any()) await TrainingData.Seed(dbContext);
                    if (!dbContext.Matches.IgnoreQueryFilters().Any()) await MatchData.Seed(dbContext);
                    if (!dbContext.MatchToPlayers.IgnoreQueryFilters().Any()) await MatchToPlayerData.Seed(dbContext);
                    if (!dbContext.Stages.IgnoreQueryFilters().Any()) await StageData.Seed(dbContext);
                    if (!dbContext.StageToPlayers.IgnoreQueryFilters().Any()) await StageToPlayerData.Seed(dbContext);
                    if (!dbContext.Balls.IgnoreQueryFilters().Any()) await BallData.Seed(dbContext);
                }
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }

            dbContext.LoggingDisable = false;
        }

        public static async Task EnsureSeededAfterAsync(this ApplicationDbContext dbContext, bool isDevelopment)
        {
            dbContext.LoggingDisable = true;

            try
            {
                //StoredFunctionData.Seed(dbContext);
                //TriggerData.Seed(dbContext);
                StoredViewData.Seed(dbContext);
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }

            dbContext.LoggingDisable = false;
        }

        public static async Task ApplyMigrationsAsync(this ApplicationDbContext dbContext)
        {
            dbContext.LoggingDisable = true;

            try
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error("{MigrationError}", ex.Message);
            }

            dbContext.LoggingDisable = false;
        }
    }
}
