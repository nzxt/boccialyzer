using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Boccialyzer.Core.SeedData
{
    public static class PlayerData
    {
        #region # Task Seed(ApplicationDbContext dbContext)

        public static async Task Seed(ApplicationDbContext dbContext)
        {
            try
            {
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "Json", "Players.json");
                var entities = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(jsonPath));
                dbContext.Players.AddRange(entities);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }

            #endregion
        }
    }
}

