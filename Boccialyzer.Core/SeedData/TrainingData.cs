using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Boccialyzer.Core.SeedData
{
    public static class TrainingData
    {
        #region # Array Trainings

        private static readonly Training[] Trainings =
        {
            new Training
            {
                Id = new Guid("df344bfa-f6aa-4ee6-9e87-f436f8711506"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b")
            },
            new Training
            {
                Id = new Guid("dd1c8c0f-b5f3-47ad-9e0e-267f3b58fa37"),
                CreatedOn = DateTime.UtcNow.AddDays(-2),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b")
            },
            new Training
            {
                Id = new Guid("a37dda83-944e-4b13-8167-cab2a969bb6f"),
                CreatedOn = DateTime.UtcNow.AddDays(-3),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b")
            },
            new Training
            {
                Id = new Guid("e60ad5be-fe37-48e3-997c-f55e68e18202"),
                CreatedOn = DateTime.UtcNow.AddDays(-4),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b")
            },
            new Training
            {
                Id = new Guid("9bf64876-930b-4dcd-a5ef-4621d9d08240"),
                CreatedOn = DateTime.UtcNow.AddDays(-5),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b")
            }
        };

        #endregion
        #region # Task Seed(ApplicationDbContext dbContext)

        public static async Task Seed(ApplicationDbContext dbContext)
        {
            try
            {
                dbContext.Trainings.AddRange(Trainings);
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
