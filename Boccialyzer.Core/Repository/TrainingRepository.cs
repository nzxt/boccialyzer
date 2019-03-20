using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій тренувань
    /// </summary>
    public interface ITrainingRepository : IGenericRepository<Training> { }

    public class TrainingRepository : GenericRepository<Training>, ITrainingRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # MatchRepository constructor

        public TrainingRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion
    }
}
