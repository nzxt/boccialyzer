using Blyzer.Dal.Context;
using Blyzer.Domain.Entities;
using Blyzer.Domain.Enums;
using Blyzer.Domain.Filtering;
using Blyzer.Domain.Models;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Blyzer.Domain.Dtos;

namespace Blyzer.Repository.Repository
{
    public interface IValueRepository : IBaseRepository<Ball>
    {
        //Task<ApiResult> GetAsync(RequestParametersModel model);
    }

    public class ValueRepository : BaseRepository<Ball>, IValueRepository
    {
        #region # Local variables

        private readonly AppDbContext _dbContext;
        private readonly IUserInfo _userInfo;
        private readonly IMapper _mapper;

        #endregion
        #region # TournamentRepository constructor

        public ValueRepository(AppDbContext dbContext, IUserInfo userInfo, IMapper mapper) : base(dbContext, userInfo, mapper)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
            _mapper = mapper;
        }

        #endregion


    }

}
