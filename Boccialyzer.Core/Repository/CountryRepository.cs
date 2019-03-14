using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій громадянства
    /// </summary>
    public interface ICountryRepository : IGenericRepository<Country>
    {
    }

    /// <summary>
    /// Репозиторій громадянства
    /// </summary>
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # CountryRepository constructor

        public CountryRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion
    }
}
