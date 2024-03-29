﻿using Boccialyzer.Core.Context;
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
    public static class TrainingData
    {
        #region # Task Seed(ApplicationDbContext dbContext)

        public static async Task Seed(ApplicationDbContext dbContext)
        {
            try
            {
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "Json", "Training.json");
                var entities = JsonConvert.DeserializeObject<List<Training>>(File.ReadAllText(jsonPath));

                dbContext.Trainings.AddRange(entities);
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
