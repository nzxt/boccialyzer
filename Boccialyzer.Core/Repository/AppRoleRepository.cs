using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Boccialyzer.Core.Context;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій системної ролі
    /// </summary>
    public interface IAppRoleRepository
    {
        #region # Task<(OperationResult Result, PagedList<AppRole> Value, string Message)> GetPagedAsync(int pageNumber, int pageSize, string filter, string order)

        /// <summary>
        /// Отримати системні ролі
        /// </summary>
        /// <param name="pageNumber">Номер сторінки</param>
        /// <param name="pageSize">Розмір сторінки</param>
        /// <param name="filter">Фільтр</param>
        /// <param name="order">Сортування</param>
        /// <returns>Список системних ролей</returns>
        Task<(OperationResult Result, PagedList<AppRole> Value, string Message)> GetPagedAsync(int pageNumber,
            int pageSize, string filter, string order);

        #endregion
        #region # Task<(OperationResult Result, AppRole Value, string Message)> GetByIdAsync(string roleId)

        /// <summary>
        /// Отримати деталі системної ролі
        /// </summary>
        /// <param name="roleId">Ідентифікатор ролі</param>
        /// <returns>Системна роль</returns>
        Task<(OperationResult Result, AppRole Value, string Message)> GetByIdAsync(string roleId);

        #endregion
        #region # Task<(OperationResult Result, AppRole Value, string Message)> GetByIdAsync(Guid roleId)

        /// <summary>
        /// Отримати роль за ідентифікатором
        /// </summary>
        /// <param name="roleId">Ідентифікатор ролі</param>
        /// <returns>Системна роль</returns>
        Task<(OperationResult Result, AppRole Value, string Message)> GetByIdAsync(Guid roleId);

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(AppRole item)

        /// <summary>
        /// Створити нову системну роль
        /// </summary>
        /// <param name="item">Нова роль (AppRole)</param>
        /// <returns>Результат виконання операції (IdentityResult)</returns>
        Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(AppRole item);

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(AppRole item)

        /// <summary>
        /// Модифікація системної ролі
        /// </summary>
        /// <param name="item">Системна роль (AppRole)</param>
        /// <returns>Результат виконання операції (IdentityResult)</returns>
        Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(AppRole item);

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> GetDefaultIdAsync()

        /// <summary>
        /// Отримати ідентифікатор ролі за замовчуванням
        /// </summary>
        /// <returns></returns>
        Task<(OperationResult Result, Guid Value, string Message)> GetDefaultIdAsync();

        #endregion
        #region # Task<(OperationResult Result, AppRole Value, string Message)> GetDefaultAsync()

        /// <summary>
        /// Отримати роль за замовчуванням
        /// </summary>
        /// <returns></returns>
        Task<(OperationResult Result, AppRole Value, string Message)> GetDefaultAsync();

        #endregion
        #region # Task<(OperationResult Result, string Value, string Message)> GetNameByIdAsync(Guid id)

        /// <summary>
        /// Отримати назву системної ролі
        /// </summary>
        /// <param name="id">Ідентифікатор ролі</param>
        /// <returns>Назва системної ролі</returns>
        Task<(OperationResult Result, string Value, string Message)> GetNameByIdAsync(Guid id);
        
        #endregion
        #region # Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(string roleId)

        /// <summary>
        /// Чи існує роль
        /// </summary>
        /// <param name="roleId">Ідентифікатор ролі (string)</param>
        /// <returns>TRUE - роль з таким ідентифікатором існує</returns>
        /// <returns>FALSE - роль з таким ідентифікатором відсутня</returns>
        Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(string roleId);

        #endregion
        #region # Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(Guid roleId)

        /// <summary>
        /// Чи існує роль
        /// </summary>
        /// <param name="roleId">Ідентифікатор ролі (Guid)</param>
        /// <returns>TRUE - роль з таким ідентифікатором існує</returns>
        /// <returns>FALSE - роль з таким ідентифікатором відсутня</returns>
        Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(Guid roleId);

        #endregion
    }

    /// <summary>
    /// Репозиторій системної ролі
    /// </summary>
    public class AppRoleRepository : IAppRoleRepository
    {
        #region # Local variables

        private readonly RoleManager<AppRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # AppRoleRepository constructor

        /// <summary>
        /// AppRoleRepository constructor
        /// </summary>
        /// <param name="roleManager">Системна роль</param>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="userInfo">IUserInfo</param>
        public AppRoleRepository(ApplicationDbContext dbContext, RoleManager<AppRole> roleManager, IUserInfo userInfo)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userInfo = userInfo;
        }

        #endregion

        #region # Task<(OperationResult Result, PagedList<AppRole> Value, string Message)> GetPagedAsync(int pageNumber, int pageSize, string filter, string order)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, PagedList<AppRole> Value, string Message)> GetPagedAsync(int pageNumber, int pageSize, string filter, string order)
        {
            try
            {
                var result = await _roleManager.Roles.AsNoTracking().Paged(pageNumber, pageSize, filter, order);
                return (Result: OperationResult.Ok, Value: result, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, AppRole Value, string Message)> GetByIdAsync(string roleId)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, AppRole Value, string Message)> GetByIdAsync(string roleId)
        {
            try
            {
                var dbResult = await _roleManager.FindByIdAsync(roleId);
                return (Result: OperationResult.Ok, Value: dbResult, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, AppRole Value, string Message)> GetByIdAsync(Guid roleId)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, AppRole Value, string Message)> GetByIdAsync(Guid roleId)
        {
            try
            {
                var dbResult = await _roleManager.FindByIdAsync(roleId.ToString());
                return (Result: OperationResult.Ok, Value: dbResult, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(AppRole item)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(AppRole item)
        {
            try
            {
                //if (item.IsDefault) await _roleManager.Roles.ForEachAsync(x => x.IsDefault = false);
                _dbContext.LoggingDisable = true;
                _dbContext.SaveChanges();
                _dbContext.LoggingDisable = false;

                item.CreatedOn = DateTime.UtcNow;
                item.CreatedBy = _userInfo.AppUserId;

                var result = await _roleManager.CreateAsync(item);
                if (result.Succeeded)
                    return (Result: OperationResult.Ok, Value: item.Id, Message: "");

                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");

            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(AppRole item)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(AppRole item)
        {
            try
            {
                if (item.IsDefault) await _dbContext.Roles.ForEachAsync(x => x.IsDefault = false);

                var role = await _roleManager.FindByIdAsync(item.Id.ToString());
                role.Name = item.Name;
                role.Caption = item.Caption;
                role.IsDefault = item.IsDefault;

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return (Result: OperationResult.Ok, Value: item.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "[AppRole.UpdateAsync] Щось пішло не за планом...");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, Guid Value, string Message)> GetDefaultIdAsync()

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> GetDefaultIdAsync()
        {
            try
            {
                var result = await _dbContext.Roles.FirstOrDefaultAsync(x => x.IsDefault);
                if (result != null) return (Result: OperationResult.Ok, Value: result.Id, Message: "");
                return (Result: OperationResult.Ok, Value: default(Guid), Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, AppRole Value, string Message)> GetDefaultAsync()

        /// <inheritdoc/>
        public async Task<(OperationResult Result, AppRole Value, string Message)> GetDefaultAsync()
        {
            try
            {
                var result = await _dbContext.Roles.FirstOrDefaultAsync(x => x.IsDefault);
                if (result != null) return (Result: OperationResult.Ok, Value: result, Message: "");
                return (Result: OperationResult.Ok, Value: null, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, string Value, string Message)> GetNameByIdAsync(Guid id)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, string Value, string Message)> GetNameByIdAsync(Guid id)
        {
            try
            {
                var result = await _dbContext.Roles.FindAsync(id);
                if (result != null) return (Result: OperationResult.Ok, Value: result.Name, Message: "");
                return (Result: OperationResult.Ok, Value: "", Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: "", Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(string roleId)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(string roleId)
        {
            try
            {
                var result = await _roleManager.FindByIdAsync(roleId);
                if (result != null) return (Result: OperationResult.Ok, Value: true, Message: "");
                return (Result: OperationResult.Ok, Value: false, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: false, Message: ex.Message); }
        }

        #endregion
        #region # Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(Guid roleId)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(Guid roleId)
        {
            try
            {
                var result = await _dbContext.Roles.FindAsync(roleId);
                if (result != null) return (Result: OperationResult.Ok, Value: true, Message: "");
                return (Result: OperationResult.Ok, Value: false, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: false, Message: ex.Message); }
        }

        #endregion
    }
}