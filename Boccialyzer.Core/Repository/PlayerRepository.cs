using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій гравців
    /// </summary>
    public interface IPlayerRepository : IGenericRepository<Player> { }

    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # MatchRepository constructor

        public PlayerRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion

        #region Task<(OperationResult Result, PagedList<TEntity> Value, string Message)> GetPaged(int pageNumber, int pageSize, string filter, string order)

        /// <inheritdoc/>
        public new async Task<(OperationResult Result, PagedList<Player> Value, string Message)> GetPaged(int pageNumber, int pageSize, string filter, string order)
        {
            try
            {
                var qry = _dbContext.Set<Player>().AsNoTracking().AsQueryable().Where(x => x.IsBisFed || x.CreatedBy == _userInfo.AppUserId);

                if (!string.IsNullOrEmpty(filter.Trim()))
                {
                    LambdaExpression lambdaExpression = DynamicExpressionParser.ParseLambda(
                        typeof(Player), typeof(bool),
                        filter.Trim());
                    qry = qry.Where("@0(it)", lambdaExpression);
                }

                qry = string.IsNullOrEmpty(order.Trim()) ? qry.OrderBy(x => x.Id) : qry.OrderBy(order.Trim());

                var result = new PagedList<Player>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    ItemCount = await qry.CountAsync(),
                    Items = await qry.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
                };
                result.PageCount = (int)Math.Ceiling((double)result.ItemCount / pageSize);
                if (pageNumber == 1)
                {
                    result.HasPreviousPage = false;
                    result.IsFirstPage = true;
                }
                else
                {
                    result.HasPreviousPage = true;
                    result.IsFirstPage = false;
                }
                if (pageNumber == result.PageCount)
                {
                    result.HasNextPage = false;
                    result.IsLastPage = true;
                }
                else
                {
                    result.HasNextPage = true;
                    result.IsLastPage = false;
                }
                return (Result: OperationResult.Ok, Value: result, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
    }
}
