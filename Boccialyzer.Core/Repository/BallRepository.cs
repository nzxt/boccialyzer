using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій м'ячів
    /// </summary>
    public interface IBallRepository : IGenericRepository<Ball> { }

   public class BallRepository : GenericRepository<Ball>, IBallRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # BallRepository constructor

        public BallRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion
    }
}
