using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій етапів
    /// </summary>
    public interface IStageRepository : IGenericRepository<Stage>
    {
        new Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(Stage entity);
    }

    public class StageRepository : GenericRepository<Stage>, IStageRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;
        private readonly IMatchRepository _matchRepository;

        #endregion
        #region # StageRepository constructor

        public StageRepository(ApplicationDbContext dbContext, IUserInfo userInfo, IMatchRepository matchRepository) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
            _matchRepository = matchRepository;
        }

        #endregion

        public new async Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(Stage entity)
        {
            try
            {
                var filterRed = new List<Box> { Box.Box1, Box.Box3, Box.Box5 };
                var filterBlue = new List<Box> { Box.Box2, Box.Box4, Box.Box6 };
                var ballsRed = entity.Balls.Where(_ => filterRed.Contains(_.Box)).ToList();
                var ballsBlue = entity.Balls.Where(_ => filterBlue.Contains(_.Box)).ToList();

                entity.AvgPointRed = ballsRed.Any() ? (int)((decimal)ballsRed.Sum(x => x.Rating) / ballsRed.Count() / 5 * 100) : 0;
                entity.AvgPointBlue = ballsBlue.Any() ? (int)((decimal)ballsBlue.Sum(x => x.Rating) / ballsBlue.Count() / 5 * 100) : 0;

                var result = await base.CreateAsync(entity);

                var match = _dbContext.Matches.Find(entity.MatchId);
                if (match != null)
                {
                    var updResult = await _matchRepository.UpdateAsync(match);
                    if (updResult.Result == OperationResult.Error)
                        return (Result: OperationResult.Error, Value: default(Guid), Message: updResult.Message);
                }
                else
                    return (Result: OperationResult.Error, Value: default(Guid), Message: "Матч не знайдено.");

                return result;
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }
    }
}
