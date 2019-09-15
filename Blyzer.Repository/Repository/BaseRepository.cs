using Blyzer.Dal.Context;
using Blyzer.Domain.Entities;
using Blyzer.Domain.Enums;
using Blyzer.Domain.Extensions;
using Blyzer.Domain.Filtering;
using Blyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Blyzer.Repository.Repository
{
    /// <summary>
    /// Common Repository
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    public interface IBaseRepository<in TEntity> where TEntity : class, IBaseEntity
    {
        #region # GetById(Guid id)

        /// <summary>
        /// Get entity detail by ID
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>ApiResult</returns>
        Task<ApiResult> GetByIdAsync(Guid id);

        #endregion
        #region # Task<ApiResult> CreateAsync(TEntity entity)

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>ApiResult</returns>
        Task<ApiResult> CreateAsync(TEntity entity);

        #endregion
        #region # Task<ApiResult> UpdateAsync(TEntity entity)

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Modified entity</param>
        /// <returns>ApiResult</returns>
        Task<ApiResult> UpdateAsync(TEntity entity);

        #endregion
        #region # DeleteAsync(Guid id)

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>ApiResult</returns>
        Task<ApiResult> DeleteAsync(Guid id);

        #endregion
        #region # GetAsync(RequestParametersModel)

        /// <summary>
        /// Get entities list with filtering, sorting, pagination
        /// </summary>
        /// <param name="model">Input parameters model</param>
        /// <returns>ApiResult</returns>
        Task<ApiResult> GetAsync<TDto>(RequestParametersModel model);

        #endregion
        #region # GetAsync()

        /// <summary>
        /// Get entities list (ALL!)
        /// </summary>
        /// <returns>ApiResult</returns>
        Task<ApiResult> GetAsync();
        #endregion
    }

    /// <summary>
    /// Common Repository
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        #region # Local variables

        private readonly AppDbContext _dbContext;
        private readonly IUserInfo _userInfo;
        private readonly IMapper _mapper;

        #endregion
        #region GenericRepository constructor

        /// <summary>
        /// GenericRepository constructor
        /// </summary>
        /// <param name="dbContext">DB context</param>
        /// <param name="userInfo">userInfo</param>
        public BaseRepository(AppDbContext dbContext, IUserInfo userInfo, IMapper mapper)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
            _mapper = mapper;
        }
        #endregion

        #region # GetByIdAsync(Guid id)

        /// <inheritdoc/>
        public virtual async Task<ApiResult> GetByIdAsync(Guid id)
        {
            try
            {
                var dbResult = await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return new ApiResult(status: ApiResultStatus.Ok, result: dbResult);
            }
            catch (Exception ex)
            { return new ApiResult(status: ApiResultStatus.Error, result: null, error: ex.Message); }
        }

        #endregion
        #region # CreateAsync(TEntity entity)

        /// <inheritdoc/>
        public virtual async Task<ApiResult> CreateAsync(TEntity entity)
        {
            try
            {
                entity.CreatedOn = DateTime.UtcNow;
                entity.CreatedBy = _userInfo.AppUserId;
                await _dbContext.Set<TEntity>().AddAsync(entity);
                var dbResult = await _dbContext.SaveChangesAsync();
                if (dbResult >= 1)
                    return new ApiResult(status: ApiResultStatus.Ok, result: dbResult);
                return new ApiResult(status: ApiResultStatus.Error, result: null, error: "Something went wrong.");
            }
            catch (Exception ex)
            { return new ApiResult(status: ApiResultStatus.Error, result: null, error: ex.Message); }
        }

        #endregion
        #region # UpdateAsync(TEntity entity)

        /// <inheritdoc/>
        public virtual async Task<ApiResult> UpdateAsync(TEntity entity)
        {
            try
            {
                var exist = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
                if (exist == null)
                    return new ApiResult(status: ApiResultStatus.Error, result: null, error: "Entity is not found.");
                entity.UpdatedOn = DateTime.UtcNow;
                entity.UpdatedBy = _userInfo.AppUserId;
                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
                var dbResult = await _dbContext.SaveChangesAsync();
                if (dbResult >= 1)
                    return new ApiResult(status: ApiResultStatus.Ok, result: dbResult);
                return new ApiResult(status: ApiResultStatus.Error, result: null, error: "Something went wrong.");
            }
            catch (Exception ex)
            { return new ApiResult(status: ApiResultStatus.Error, result: null, error: ex.Message); }
        }

        #endregion
        #region # DeleteAsync(Guid id)

        /// <inheritdoc/>
        public virtual async Task<ApiResult> DeleteAsync(Guid id)
        {
            try
            {
                var entity = _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);
                if (entity == null)
                    return new ApiResult(status: ApiResultStatus.Error, result: null, error: "Entity is not found.");
                _dbContext.Set<TEntity>().Remove(entity);
                var dbResult = await _dbContext.SaveChangesAsync();
                if (dbResult >= 1)
                    return new ApiResult(status: ApiResultStatus.Ok, result: dbResult);
                return new ApiResult(status: ApiResultStatus.Error, result: null, error: "Something went wrong.");

            }
            catch (Exception ex)
            { return new ApiResult(status: ApiResultStatus.Error, result: null, error: ex.Message); }
        }

        #endregion

        #region # GetAsync(RequestParametersModel)

        /// <inheritdoc/>
        public virtual async Task<ApiResult> GetAsync<TDto>(RequestParametersModel model)
        {
            try
            {
                var result = await GetAllQueryable()
                    .ApplyFiltering(model.GetFiltersParsed())
                    .ApplySorting(model.GetSortsParsed())
                    .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                    .ApplyPagination(model);

                return new ApiResult(status: ApiResultStatus.Ok, result: result);
            }
            catch (Exception ex)
            { return new ApiResult(status: ApiResultStatus.Error, error: ex.Message); }
        }

        #endregion

        #region # GetAsync()

        /// <inheritdoc/>
        public async Task<ApiResult> GetAsync()
        {
            try
            {
                return new ApiResult(status: ApiResultStatus.Ok, result: await GetAllQueryable().ToListAsync());
            }
            catch (Exception ex)
            { return new ApiResult(status: ApiResultStatus.Error, error: ex.Message); }
        }

        #endregion

        /*
        public virtual void AddRange(IEnumerable<TEntity> entityList)
        {
            using (var entityContext = ContextCreator())
            {
                entityContext.Set<TEntity>().AddRange(entityList);
                entityContext.SaveChanges();
            }
        }
        public virtual void RemoveRange(IEnumerable<string> clauses)
        {

            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();

                foreach (var clause in clauses)
                    temporaryQuery = temporaryQuery.Where(clause);

                entityContext.Set<TEntity>().RemoveRange(temporaryQuery);
                entityContext.SaveChanges();
            }
        }
    */

        #region ## GetAllQueryable()

        private IQueryable<TEntity> GetAllQueryable()
        {
            return _dbContext.Set<TEntity>().AsNoTracking().AsQueryable()
                .Where(x => _userInfo.IsAdmin || x.IsPublic || x.CreatedBy == _userInfo.AppUserId);
        }

        #endregion
    }
}