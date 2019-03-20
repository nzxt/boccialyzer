using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Зв'язок гравців з матчами/тренуваннями
    /// </summary>
    public interface IMatchToPlayerRepository : IGenericRepository<MatchToPlayer> { }

    public class MatchToPlayerRepository : GenericRepository<MatchToPlayer>, IMatchToPlayerRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # MatchRepository constructor

        public MatchToPlayerRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion
    }
}