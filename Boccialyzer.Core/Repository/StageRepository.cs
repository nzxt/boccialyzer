using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій етапів
    /// </summary>
    public interface IStageRepository : IGenericRepository<Stage> { }

   public class StageRepository : GenericRepository<Stage>, IStageRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # StageRepository constructor

        public StageRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion
    }
}
