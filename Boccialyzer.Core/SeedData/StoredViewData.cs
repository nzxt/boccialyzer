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
    public class StoredViewData
    {
        #region # Task Seed(ApplicationDbContext dbContext)

        public static void Seed(ApplicationDbContext dbContext)
        {
            dbContext.LoggingDisable = true;

            try
            {
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "Json", "StoredView.json");

                if (File.Exists(jsonPath))
                {
                    var spData = JsonConvert.DeserializeObject<List<StoredFunctionDto>>(File.ReadAllText(jsonPath));
                    var storedFunctions = dbContext.Query<ViewStoredView>().FromSql(@"SELECT viewname FROM pg_catalog.pg_views WHERE viewname LIKE 'boccialyzer_%' ORDER BY viewname ASC").ToList();

                    foreach (var item in spData)
                    {
                        var resultCount = storedFunctions.Count(x => x.Viewname == item.Proname);
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
