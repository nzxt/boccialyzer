using Microsoft.EntityFrameworkCore;
using Boccialyzer.Core.Context;
using Boccialyzer.Domain;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Загальний репозиторій
    /// </summary>
    /// <typeparam name="TEntity">Сутність</typeparam>
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        //#region # Task<(OperationResult Result, IList<TEntity> Value, string Message)> GetAllAsync()

        ///// <summary>
        ///// Отримати всі записи сутності
        ///// </summary>
        ///// <returns>Список екземплярів сутності</returns>
        //Task<(OperationResult Result, IList<TEntity> Value, string Message)> GetAllAsync();

        //#endregion
        #region # (OperationResult Result, TEntity Value, string Message) GetById(Guid id)

        /// <summary>
        /// Отримати деталі сутності за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <returns>Сутність</returns>
        (OperationResult Result, TEntity Value, string Message) GetById(Guid id);

        #endregion
        #region # Task<(OperationResult Result, TEntity Value, string Message)> GetByIdAsync(Guid id)

        /// <summary>
        /// Отримати деталі сутності за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <returns>Сутність</returns>
        Task<(OperationResult Result, TEntity Value, string Message)> GetByIdAsync(Guid id);

        #endregion
        #region # (OperationResult Result, Guid Value, string Message) Create(TEntity entity)

        /// <summary>
        /// Створити новий екземпляр сутності
        /// </summary>
        /// <param name="entity">Сутність</param>
        /// <returns>Ідентифікатор</returns>
        (OperationResult Result, Guid Value, string Message) Create(TEntity entity);

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(TEntity entity)

        /// <summary>
        /// Створити новий екземпляр сутності
        /// </summary>
        /// <param name="entity">Сутність</param>
        /// <returns>Ідентифікатор</returns>
        Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(TEntity entity);

        #endregion
        #region # (OperationResult Result, Guid Value, string Message) Update(TEntity entity)

        /// <summary>
        /// Модифікація екземпляру сутності
        /// </summary>
        /// <param name="entity">Екземпляр сутності</param>
        /// <returns>Ідентифікатор</returns>
        (OperationResult Result, Guid Value, string Message) Update(TEntity entity);

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(TEntity entity)

        /// <summary>
        /// Модифікація екземпляру сутності
        /// </summary>
        /// <param name="entity">Екземпляр сутності</param>
        /// <returns>Ідентифікатор</returns>
        Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(TEntity entity);

        #endregion
        #region # (OperationResult Result, Guid Value, string Message) Delete(Guid id)

        /// <summary>
        /// Видалення екземпляру сутності
        /// </summary>
        /// <param name="id">Ідентифікатор екземпляру сутності</param>
        /// <returns>Ідентифікатор</returns>
        (OperationResult Result, Guid Value, string Message) Delete(Guid id);

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> DeleteAsync(Guid id)

        /// <summary>
        /// Видалення екземпляру сутності
        /// </summary>
        /// <param name="id">Ідентифікатор екземпляру сутності</param>
        /// <returns>Ідентифікатор</returns>
        Task<(OperationResult Result, Guid Value, string Message)> DeleteAsync(Guid id);

        #endregion
        #region # Task<(OperationResult Result, PagedList<TEntity> Value, string Message)> GetPaged(int pageNumber, int pageSize, string filter, string order)

        /// <summary>
        /// Отримати список екземплярів сутності з пагінацією, фільтрацією та сортуванням
        /// </summary>
        /// <param name="pageNumber">Номер поточної сторінки</param>
        /// <param name="pageSize">Розмір сторінки</param>
        /// <param name="filter">Фільтр</param>
        /// <param name="order">Сортування</param>
        /// <returns>Список екземплярів сутності</returns>
        Task<(OperationResult Result, PagedList<TEntity> Value, string Message)> GetPaged(int pageNumber, int pageSize, string filter, string order);

        #endregion
    }

    /// <summary>
    /// Загальний репозиторій
    /// </summary>
    /// <typeparam name="TEntity">Сутність</typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region GenericRepository конструктор

        /// <summary>
        /// GenericRepository конструктор
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="userInfo">userInfo</param>
        public GenericRepository(ApplicationDbContext dbContext, IUserInfo userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion

        //#region Task<(OperationResult Result, IList<TEntity> Value, string Message)> GetAllAsync()

        ///// <inheritdoc/>
        //public async Task<(OperationResult Result, IList<TEntity> Value, string Message)> GetAllAsync()
        //{
        //    try
        //    {
        //        var dbResult = await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        //        return (Result: OperationResult.Ok, Value: dbResult, Message: "");
        //    }
        //    catch (Exception ex)
        //    { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        //}

        //#endregion
        #region (OperationResult Result, TEntity Value, string Message) GetById(Guid id)

        /// <inheritdoc/>
        public (OperationResult Result, TEntity Value, string Message) GetById(Guid id)
        {
            try
            {
                var dbResult = _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
                return (Result: OperationResult.Ok, Value: dbResult, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
        #region Task<(OperationResult Result, TEntity Value, string Message)> GetByIdAsync(Guid id)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, TEntity Value, string Message)> GetByIdAsync(Guid id)
        {
            try
            {
                var dbResult = await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                return (Result: OperationResult.Ok, Value: dbResult, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
        #region (OperationResult Result, Guid Value, string Message) Create(TEntity entity)

        /// <inheritdoc/>
        public (OperationResult Result, Guid Value, string Message) Create(TEntity entity)
        {
            try
            {
                entity.CreatedOn = DateTime.UtcNow;
                entity.CreatedBy = _userInfo.AppUserId;
                _dbContext.Set<TEntity>().Add(entity);
                var dbResult = _dbContext.SaveChanges();
                if (dbResult >= 1)
                    return (Result: OperationResult.Ok, Value: entity.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");

            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(TEntity entity)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(TEntity entity)
        {
            try
            {
                entity.CreatedOn = DateTime.UtcNow;
                entity.CreatedBy = _userInfo.AppUserId;
                await _dbContext.Set<TEntity>().AddAsync(entity);
                var dbResult = await _dbContext.SaveChangesAsync();
                if (dbResult >= 1)
                    return (Result: OperationResult.Ok, Value: entity.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region (OperationResult Result, Guid Value, string Message) Update(TEntity entity)

        /// <inheritdoc/>
        public (OperationResult Result, Guid Value, string Message) Update(TEntity entity)
        {
            try
            {
                var exist = _dbContext.Set<TEntity>().Find(entity.Id);
                if (exist == null)
                    return (Result: OperationResult.Error, Value: default(Guid), Message: "[Generic Repo] Сутність відсутня.");
                entity.UpdatedOn = DateTime.UtcNow;
                entity.UpdatedBy = _userInfo.AppUserId;

                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
                var dbResult = _dbContext.SaveChanges();
                if (dbResult >= 1)
                    return (Result: OperationResult.Ok, Value: entity.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");

            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(TEntity entity)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(TEntity entity)
        {
            try
            {
                var exist = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
                if (exist == null)
                    return (Result: OperationResult.Error, Value: default(Guid), Message: "[Generic Repo] Сутність відсутня.");
                entity.UpdatedOn = DateTime.UtcNow;
                entity.UpdatedBy = _userInfo.AppUserId;
                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
                var dbResult = await _dbContext.SaveChangesAsync();
                if (dbResult >= 1)
                    return (Result: OperationResult.Ok, Value: entity.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region (OperationResult Result, Guid Value, string Message) Delete(Guid id)

        /// <inheritdoc/>
        public (OperationResult Result, Guid Value, string Message) Delete(Guid id)
        {
            try
            {
                var entity = _dbContext.Set<TEntity>().Find(id);
                if (entity == null)
                    return (Result: OperationResult.Error, Value: default(Guid), Message: "[Generic Repo] Сутність відсутня.");
                _dbContext.Set<TEntity>().Remove(entity);
                var dbResult = _dbContext.SaveChanges();
                if (dbResult >= 1)
                    return (Result: OperationResult.Ok, Value: entity.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");

            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region Task<(OperationResult Result, Guid Value, string Message)> DeleteAsync(Guid id)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> DeleteAsync(Guid id)
        {
            try
            {
                var entity = _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);
                if (entity == null)
                    return (Result: OperationResult.Error, Value: default(Guid), Message: "[Generic Repo] Сутність відсутня.");
                _dbContext.Set<TEntity>().Remove(entity);
                var dbResult = await _dbContext.SaveChangesAsync();
                if (dbResult >= 1)
                    return (Result: OperationResult.Ok, Value: entity.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");

            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region Task<(OperationResult Result, PagedList<TEntity> Value, string Message)> GetPaged(int pageNumber, int pageSize, string filter, string order)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, PagedList<TEntity> Value, string Message)> GetPaged(int pageNumber, int pageSize, string filter, string order)
        {
            try
            {
                var qry = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable().Where(x => _userInfo.IsAdmin || x.CreatedBy == _userInfo.AppUserId);

                if (!string.IsNullOrEmpty(filter.Trim()))
                {
                    LambdaExpression lambdaExpression = DynamicExpressionParser.ParseLambda(
                        typeof(TEntity), typeof(bool),
                        filter.Trim());
                    qry = qry.Where("@0(it)", lambdaExpression);
                }

                qry = string.IsNullOrEmpty(order.Trim()) ? qry.OrderBy(x => x.Id) : qry.OrderBy(order.Trim());

                var result = new PagedList<TEntity>
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
