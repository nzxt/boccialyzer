using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій матчів
    /// </summary>
    public interface IMatchRepository : IGenericRepository<Match> { }

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
    }
}
