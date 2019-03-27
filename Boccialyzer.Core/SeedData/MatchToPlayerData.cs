using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Newtonsoft.Json;
using Npgsql.Bulk;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Boccialyzer.Core.SeedData
{
    public static class MatchToPlayerData
    {
        #region # Task Seed(ApplicationDbContext dbContext)

        public static async Task Seed(ApplicationDbContext dbContext)
        {
            try
            {
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "Json", "MatchToPlayer.json");
                var entities = JsonConvert.DeserializeObject<List<MatchToPlayer>>(File.ReadAllText(jsonPath));

                dbContext.MatchToPlayers.AddRange(entities);
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
