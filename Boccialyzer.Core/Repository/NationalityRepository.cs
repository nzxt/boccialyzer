using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Boccialyzer.Core.Context;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій громадянства
    /// </summary>
    public interface INationalityRepository : IGenericRepository<Nationality>
    {
        #region # (OperationResult Result, Guid Value, string Message) Create(Nationality entity)

        /// <summary>
        /// Створити новий екземпляр сутності
        /// </summary>
        /// <param name="entity">Сутність</param>
        /// <returns>Ідентифікатор</returns>
        new(OperationResult Result, Guid Value, string Message) Create(Nationality entity);

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(Nationality entity)

        /// <summary>
        /// Створити новий екземпляр сутності
        /// </summary>
        /// <param name="entity">Сутність</param>
        /// <returns>Ідентифікатор</returns>
        new Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(Nationality entity);

        #endregion
        #region # (OperationResult Result, Guid Value, string Message) Update(Nationality entity)

        /// <summary>
        /// Модифікація екземпляру сутності
        /// </summary>
        /// <param name="entity">Екземпляр сутності</param>
        /// <returns>Ідентифікатор</returns>
        new(OperationResult Result, Guid Value, string Message) Update(Nationality entity);

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(Nationality entity)

        /// <summary>
        /// Модифікація екземпляру сутності
        /// </summary>
        /// <param name="entity">Екземпляр сутності</param>
        /// <returns>Ідентифікатор</returns>
        new Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(Nationality entity);

        #endregion
    }

    /// <summary>
    /// Репозиторій громадянства
    /// </summary>
    public class NationalityRepository : GenericRepository<Nationality>, INationalityRepository
    {
        #region # Local variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # NationalityRepository constructor

        public NationalityRepository(ApplicationDbContext dbContext, IUserInfo userInfo) : base(dbContext, userInfo)
        {
            _dbContext = dbContext;
            _userInfo = userInfo;
        }

        #endregion

        #region # (OperationResult Result, Guid Value, string Message) Create(Nationality entity)

        /// <inheritdoc/>
        public new(OperationResult Result, Guid Value, string Message) Create(Nationality entity)
        {
            try
            {
                if (entity.IsDefault) _dbContext.Nationalities.ForEachAsync(x => x.IsDefault = false).Wait();
                var result = base.Create(entity);
                if (result.Result == OperationResult.Ok) return (Result: OperationResult.Ok, Value: result.Value, Message: "");
                return (Result: OperationResult.Error, Value: result.Value, Message: result.Message);
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(Nationality entity)

        /// <inheritdoc/>
        public new async Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(Nationality entity)
        {
            try
            {
                if (entity.IsDefault) await _dbContext.Nationalities.ForEachAsync(x => x.IsDefault = false);
                var result = await base.CreateAsync(entity);
                if (result.Result == OperationResult.Ok) return (Result: OperationResult.Ok, Value: result.Value, Message: "");
                return (Result: OperationResult.Error, Value: result.Value, Message: result.Message);
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # (OperationResult Result, Guid Value, string Message) Update(Nationality entity)

        /// <inheritdoc/>
        public new(OperationResult Result, Guid Value, string Message) Update(Nationality entity)
        {
            try
            {
                if (entity.IsDefault) _dbContext.Nationalities.ForEachAsync(x => x.IsDefault = false).Wait();
                var result = base.Update(entity);
                if (result.Result == OperationResult.Ok) return (Result: OperationResult.Ok, Value: result.Value, Message: "");
                return (Result: OperationResult.Error, Value: result.Value, Message: result.Message);
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(Nationality entity)

        /// <inheritdoc/>
        public new async Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(Nationality entity)
        {
            try
            {
                if (entity.IsDefault) await _dbContext.Nationalities.ForEachAsync(x => x.IsDefault = false);
                var result = await base.UpdateAsync(entity);
                if (result.Result == OperationResult.Ok) return (Result: OperationResult.Ok, Value: result.Value, Message: "");
                return (Result: OperationResult.Error, Value: result.Value, Message: result.Message);
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
    }
}
