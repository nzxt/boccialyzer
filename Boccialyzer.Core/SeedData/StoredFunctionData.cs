using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Dtos;
using Boccialyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Boccialyzer.Core.SeedData
{
    public class StoredFunctionData
    {
        #region # Task Seed(ApplicationDbContext dbContext)

        public static void Seed(ApplicationDbContext dbContext)
        {
            dbContext.LoggingDisable = true;

            try
            {
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "Json", "StoredFunction.json");

                if (File.Exists(jsonPath))
                {
                    var spData = JsonConvert.DeserializeObject<List<StoredFunctionDto>>(File.ReadAllText(jsonPath));
                    var storedFunctions = dbContext.Query<ViewStoredFunction>().FromSql(@"SELECT proname FROM pg_proc WHERE proname LIKE 'boccialyzer_%' ORDER BY proname ASC").ToList();

                    foreach (var item in spData)
                    {
                        var resultCount = storedFunctions.Count(x => x.Proname == item.Proname);
                        if (resultCount == 0) dbContext.Database.ExecuteSqlCommand(item.Sql);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }

            dbContext.LoggingDisable = false;
        }

        #endregion
    }
}
