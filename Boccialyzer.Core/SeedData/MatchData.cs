using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Boccialyzer.Core.SeedData
{
    public static class MatchData
    {
        #region # Array Matches

        private static readonly Match[] Matches =
        {
            new Match
            {
                Id = new Guid("2208aed9-f8be-45ce-aefa-631be568a345"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = default(Guid),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                MatchType=MatchType.None,
                CompetitionEvent=CompetitionEvent.None,
                PoolStage=PoolStage.PoolA,
                EliminationStage=EliminationStage.None,
                ScoreRed=0,
                ScoreBlue=0,
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                TrainingId=new Guid(""),
                TournamentId=new Guid("")
            },
            new Match
            {
                Id = new Guid("7673bc60-7687-4967-a00f-9a864cba3c97"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = default(Guid),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                MatchType=MatchType.None,
                CompetitionEvent=CompetitionEvent.None,
                PoolStage=PoolStage.PoolA,
                EliminationStage=EliminationStage.None,
                ScoreRed=0,
                ScoreBlue=0,
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                TrainingId=new Guid(""),
                TournamentId=new Guid("")
            },
            new Match
            {
                Id = new Guid("16d4cd44-a1b5-449f-99ba-676de40f54be"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = default(Guid),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                MatchType=MatchType.None,
                CompetitionEvent=CompetitionEvent.None,
                PoolStage=PoolStage.PoolA,
                EliminationStage=EliminationStage.None,
                ScoreRed=0,
                ScoreBlue=0,
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                TrainingId=new Guid(""),
                TournamentId=new Guid("")
            },
            new Match
            {
                Id = new Guid("4a766f82-7d16-4172-a1cf-f79998f4f7e4"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = default(Guid),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                MatchType=MatchType.None,
                CompetitionEvent=CompetitionEvent.None,
                PoolStage=PoolStage.PoolA,
                EliminationStage=EliminationStage.None,
                ScoreRed=0,
                ScoreBlue=0,
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                TrainingId=new Guid(""),
                TournamentId=new Guid("")
            },
            new Match
            {
                Id = new Guid("644e4cca-c319-4e78-90f0-b0e8656d4466"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = default(Guid),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                MatchType=MatchType.None,
                CompetitionEvent=CompetitionEvent.None,
                PoolStage=PoolStage.PoolA,
                EliminationStage=EliminationStage.None,
                ScoreRed=0,
                ScoreBlue=0,
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                TrainingId=new Guid(""),
                TournamentId=new Guid("")
            },
            new Match
            {
                Id = new Guid("30016030-ea1c-4cde-a9a2-e509995860c3"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = default(Guid),
                DateTimeStamp=DateTime.UtcNow.AddDays(-1),
                MatchType=MatchType.None,
                CompetitionEvent=CompetitionEvent.None,
                PoolStage=PoolStage.PoolA,
                EliminationStage=EliminationStage.None,
                ScoreRed=0,
                ScoreBlue=0,
                AppUserId=new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                TrainingId=new Guid(""),
                TournamentId=new Guid("")
            }
        };

        #endregion
        #region # Task Seed(ApplicationDbContext dbContext)

        public static async Task Seed(ApplicationDbContext dbContext)
        {
            try
            {
                dbContext.Matches.AddRange(Matches);
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
