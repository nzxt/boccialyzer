using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Dtos;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій тренувань
    /// </summary>
    public interface ITrainingRepository : IGenericRepository<Training>
    {
        /// <summary>
        /// Отримати тренування (пагінація)
        /// </summary>
        /// <param name="pageNumber">Номер поточної сторінки</param>
        /// <param name="pageSize">Розмір сторінки</param>
        /// <param name="filter">Фільтр</param>
        /// <param name="order">Сортування</param>
        /// <returns>Список екземплярів сутності</returns>
        new Task<(OperationResult Result, PagedList<TrainingDto> Value, string Message)> GetPaged(int pageNumber,
            int pageSize, string filter, string order);

        /// <summary>
        /// Отримати статистику тренування за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <returns>Статистика тренування</returns>
        new Task<(OperationResult Result, IEnumerable<StatResultTraining> Value, string Message)> GetByIdAsync(Guid id);
    }

    public class TrainingRepository : GenericRepository<Training>, ITrainingRepository
    {
        #region # Local variables

        private readonly AppOptionModel _appOptions;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # TrainingRepository constructor

        public TrainingRepository(IOptionsSnapshot<AppOptionModel> appOptions, ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _appOptions = appOptions.Value;
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion

        #region Task<(OperationResult Result, PagedList<TrainingDto> Value, string Message)> GetPaged(int pageNumber, int pageSize, string filter, string order)

        /// <inheritdoc/>
        public new async Task<(OperationResult Result, PagedList<TrainingDto> Value, string Message)> GetPaged(int pageNumber, int pageSize, string filter, string order)
        {
            try
            {
                var qry = _dbContext.Set<Training>().AsNoTracking().AsQueryable().Where(x => _userInfo.IsAdmin || x.CreatedBy == _userInfo.AppUserId);

                if (!string.IsNullOrEmpty(filter.Trim()))
                {
                    LambdaExpression lambdaExpression = DynamicExpressionParser.ParseLambda(
                        typeof(Training), typeof(bool),
                        filter.Trim());
                    qry = qry.Where("@0(it)", lambdaExpression);
                }

                qry = string.IsNullOrEmpty(order.Trim()) ? qry.OrderBy(x => x.Id) : qry.OrderBy(order.Trim());

                var resDto = qry.Select(_ => new TrainingDto
                {
                    Id = _.Id,
                    DateTimeStamp = _.DateTimeStamp,
                    TrainingType = _.TrainingType,
                    AvgBallRating = Math.Round(_.Balls.Average(x => x.Rating), 2),
                    Rate = _.Rate
                });

                var result = new PagedList<TrainingDto>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    ItemCount = await qry.CountAsync(),
                    Items = await resDto.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
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
        #region Task<(OperationResult Result, TrainingStatDto Value, string Message)> GetByIdAsync(Guid id)

        /// <inheritdoc/>
        public new async Task<(OperationResult Result, IEnumerable<StatResultTraining> Value, string Message)> GetByIdAsync(Guid id)
        {

            using (IDbConnection db = new NpgsqlConnection(_appOptions.ConnectionString))
            {
                try
                {
                    db.Open();
                    var dbResult = await db.QueryAsync<StatResultTraining>("public.boccialyzer_get_training_stat", new
                    {
                        createdby = _userInfo.AppUserId,
                        trainingid = id
                    }, commandType: CommandType.StoredProcedure);
                    db.Close();
                    if (dbResult == null)
                        return (Result: OperationResult.Error, Value: null, Message: "[GetByIdAsync] Щось пішло не за планом...");
                    return (Result: OperationResult.Ok, Value: dbResult, Message: "");
                }
                catch (Exception ex)
                {
                    Log.Error("{SqlError}", ex.Message);
                    return (Result: OperationResult.Error, Value: null, Message: ex.Message);
                }
            }
        }

        #endregion
    }
}
