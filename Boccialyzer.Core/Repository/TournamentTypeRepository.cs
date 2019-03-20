using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій турнирів
    /// </summary>
    public interface ITournamentTypeRepository : IGenericRepository<TournamentType> { }

    public class TournamentTypeRepository : GenericRepository<TournamentType>, ITournamentTypeRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # TournamentTypeRepository constructor

        public TournamentTypeRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion
    }
}