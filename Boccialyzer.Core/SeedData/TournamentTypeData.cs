using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Boccialyzer.Core.SeedData
{
    public static class TournamentTypeData
    {
        #region # Array TournamentTypes

        private static readonly TournamentType[] TournamentTypes =
        {
            new TournamentType
            {
                Id = new Guid("c1632aa2-0387-42b9-9d31-9cbcd0560263"),
                CreatedOn = DateTime.UtcNow, 
                CreatedBy = default(Guid),
                Name = "Paralympic Games",
                Abbr="PG",
                Icon = ""
            },
            new TournamentType
            {
                Id = new Guid("bb179549-719f-42af-8239-cd8e70ff2405"),
                CreatedOn = DateTime.UtcNow,
                CreatedBy = default(Guid),
                Name = "World Championships",
                Abbr="WC",
                Icon = ""
            },
            new TournamentType
            {
                Id = new Guid("db534aeb-4f04-45a1-addb-86eff0dfff52"),
                CreatedOn = DateTime.UtcNow,
                CreatedBy = default(Guid),
                Name = "Regional Championships",
                Abbr="RC",
                Icon = ""
            },
            new TournamentType
            {
                Id = new Guid("57db3543-1f0c-4ae5-ab97-486e08ed8858"),
                CreatedOn = DateTime.UtcNow,
                CreatedBy = default(Guid),
                Name = "World Open",
                Abbr="WO",
                Icon = ""
            },
            new TournamentType
            {
                Id = new Guid("d219bba9-e8c3-4a5a-9d03-b2e13e86be63"),
                CreatedOn = DateTime.UtcNow,
                CreatedBy = default(Guid),
                Name = "Regional Open",
                Abbr="RO",
                Icon = ""
            }
        };

        #endregion
        #region # Task Seed(ApplicationDbContext dbContext)

        public static async Task Seed(ApplicationDbContext dbContext)
        {
            try
            {
                dbContext.TournamentTypes.AddRange(TournamentTypes);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }
        }

        #endregion
    }
}
