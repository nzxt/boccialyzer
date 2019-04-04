using System;
using System.Linq;
using System.Threading.Tasks;
using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій матчів
    /// </summary>
    public interface IMatchRepository : IGenericRepository<Match>
    {
        /// <summary>
        /// Модифікація екземпляру сутності
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        new Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(Match entity);
    }

   public class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # MatchRepository constructor

        public MatchRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion

        #region Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(Match entity)

        /// <inheritdoc/>
        public new async Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(Match entity)
        {
            try
            {
                var exist = await _dbContext.Set<Match>().FindAsync(entity.Id);
                if (exist == null)
                    return (Result: OperationResult.Error, Value: default(Guid), Message: "Сутність відсутня.");

                var stages = _dbContext.Stages.Where(x => x.MatchId == entity.Id);

                entity.ScoreRed = await stages.SumAsync(_ => _.ScoreRed);
                entity.ScoreBlue = await stages.SumAsync(_ => _.ScoreBlue);
                entity.AvgPointRed = (int)(await stages.AverageAsync(_ => _.AvgPointRed));
                entity.AvgPointBlue = (int)(await stages.AverageAsync(_ => _.AvgPointBlue));

                entity.UpdatedOn = DateTime.UtcNow;
                entity.UpdatedBy = _userInfo.AppUserId;
                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
                var dbResult = await _dbContext.SaveChangesAsync();
                if (dbResult >= 1)
                    return (Result: OperationResult.Ok, Value: entity.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion

    }
}
