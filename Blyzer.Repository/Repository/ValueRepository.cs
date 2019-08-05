using System;
using System.Collections.Generic;
using System.Text;
using Blyzer.Dal.Context;
using Blyzer.Domain.Entities;
using Blyzer.Domain.Models;
using Microsoft.Extensions.Options;

namespace Blyzer.Repository.Repository
{
    public interface IValueRepository : ICommonRepository<Ball> { }

    public class ValueRepository : CommonRepository<Ball>, IValueRepository
    {
        #region # Local variables

        private readonly AppDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # TournamentRepository constructor

        public ValueRepository(AppDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion
    }

}
