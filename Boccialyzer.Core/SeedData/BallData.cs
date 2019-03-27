using Boccialyzer.Core.Context;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Boccialyzer.Domain.Entities;
using Newtonsoft.Json;
using Npgsql.Bulk;

namespace Boccialyzer.Core.SeedData
{
    public static class BallData
    {
        #region # Task Seed(ApplicationDbContext dbContext)

        public static async Task Seed(ApplicationDbContext dbContext)
        {
            try
            {
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "Json", "Ball.json");
                var entities = JsonConvert.DeserializeObject<List<Ball>>(File.ReadAllText(jsonPath));

                await dbContext.Balls.AddRangeAsync(entities);
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
