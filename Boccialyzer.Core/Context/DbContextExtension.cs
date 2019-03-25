using Microsoft.EntityFrameworkCore;
using Boccialyzer.Core.SeedData;
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

                //Seed для разработчиков
                if (isDevelopment == true)
                {
                    if (!dbContext.Trainings.IgnoreQueryFilters().Any()) await TrainingData.Seed(dbContext);
                    if (!dbContext.Balls.IgnoreQueryFilters().Any()) await BallData.Seed(dbContext);
                    //if (!dbContext.Sims.IgnoreQueryFilters().Any()) await SimData.Seed(dbContext);
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

            //try
            //{
            //    //Seed для разработчиков
            //    if (isDevelopment == true)
            //    {
            //        if (!dbContext.Crimes.IgnoreQueryFilters().Any()) await CrimeData.Seed(dbContext);
            //        if (!dbContext.CrimeToCodex.IgnoreQueryFilters().Any()) await CrimeToCodexData.Seed(dbContext);
            //        if (!dbContext.CourtOrders.IgnoreQueryFilters().Any()) await CourtOrderData.Seed(dbContext);
            //        if (!dbContext.Issues.IgnoreQueryFilters().Any()) await IssueData.Seed(dbContext);
            //        if (!dbContext.Sessions.IgnoreQueryFilters().Any()) await SessionData.Seed(dbContext);
            //        if (!dbContext.Interlocutors.IgnoreQueryFilters().Any()) await InterlocutorData.Seed(dbContext);
            //        if (!dbContext.InitiatorToIssues.IgnoreQueryFilters().Any()) await InitiatorToIssueData.Seed(dbContext);
            //        if (!dbContext.SessionDescriptions.IgnoreQueryFilters().Any()) await SessionDescriptionData.Seed(dbContext);
            //        if (!dbContext.Documents.IgnoreQueryFilters().Any()) await DocumentData.Seed(dbContext);
            //        if (!dbContext.IssueStatusHistories.IgnoreQueryFilters().Any()) await IssueStatusHistoryData.Seed(dbContext);
            //    }
            //    if (!dbContext.ImporterServices.IgnoreQueryFilters().Any()) await ImporterServiceData.Seed(dbContext);

            //    StoredFunctionData.Seed(dbContext);
            //    TriggerData.Seed(dbContext);
            //    StoredViewData.Seed(dbContext);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error("{SeedError}", ex.Message);
            //}

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
