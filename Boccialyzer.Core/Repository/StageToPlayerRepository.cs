using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Зв'язок гравців з етапами
    /// </summary>
    public interface IStageToPlayerRepository : IGenericRepository<StageToPlayer> { }

    public class StageToPlayerRepository : GenericRepository<StageToPlayer>, IStageToPlayerRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # StageToPlayerRepository constructor

        public StageToPlayerRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion
    }
}