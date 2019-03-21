using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій користувача системи
    /// </summary>
    public interface IAppUserRepository
    {
        #region # Task<(...)> GetPagedAsync(...)

        /// <summary>
        /// Отримати системних користувачів
        /// </summary>
        /// <param name="pageNumber">Номер сторінки</param>
        /// <param name="pageSize">Розмір сторінки</param>
        /// <param name="filter">Фільтр</param>
        /// <param name="order">Сортування</param>
        /// <returns>Список системних користувачів</returns>
        Task<(OperationResult Result, PagedList<AppUser> Value, string Message)> GetPagedAsync(int pageNumber,
            int pageSize, string filter, string order);

        #endregion
        #region # Task<(...)> GetByIdAsync(...)

        /// <summary>
        /// Отримати деталі користувача системи
        /// </summary>
        /// <param name="id">Ідентифікатор користувача</param>
        /// <returns>Користувач системи</returns>
        Task<(OperationResult Result, AppUser Value, string Message)> GetByIdAsync(Guid id);

        #endregion
        #region # Task<(...)> CreateAsync(...)

        /// <summary>
        /// Створити нового користувача
        /// </summary>
        /// <param name="item">Модель для сворення нового користувача</param>
        /// <returns>Результат операції: ідентифікатор, результат</returns>
        Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(NewUserModel item);

        #endregion
        #region # Task<(...)> UpdateAsync(...)

        /// <summary>
        /// Модифікувати користувача
        /// </summary>
        /// <param name="item">Користувач</param>
        /// <returns>Результат</returns>
        Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(AppUser item);

        #endregion
        #region # Task<(...)> DeleteAsync(...)

        /// <summary>
        /// Видалення користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Відсутній</returns>
        Task<(OperationResult Result, Guid Value, string Message)> DeleteAsync(Guid userId);

        #endregion
        #region # Task<.....> GetByNameAsync(...)

        /// <summary>
        /// Отримати деталі користувача системи
        /// </summary>
        /// <param name="userName">Ім'я користувача</param>
        /// <returns>Користувач системи</returns>
        Task<AppUser> GetByNameAsync(string userName);

        #endregion
        #region # Task<.....> GetUserRoleAsync(...)

        /// <summary>
        /// Отримати всі ролі користівача
        /// </summary>
        /// <param name="userName">Ім'я користувача</param>
        /// <returns>Список ролей</returns>
        Task<IList<string>> GetUserRoleAsync(string userName);

        #endregion
        #region # Task<(...)> IsExistAsync(...)

        /// <summary>
        /// Чи існує користувач
        /// </summary>
        /// <param name="userName">Ім'я користувача</param>
        /// <returns>TRUE - користувач з таким ім'ям існує</returns>
        /// <returns>FALSE - користувача з таким ім'ям не існує</returns>
        Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(string userName);

        #endregion
        #region # Task<(...)> IsExistAsync(...)

        /// <summary>
        /// Чи існує користувач
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>TRUE - користувач з таким ім'ям існує</returns>
        /// <returns>FALSE - користувача з таким ім'ям не існує</returns>

        Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(Guid userId);


        #endregion
        #region # Task<(...)> GetIdByUserNameAsync(...)

        /// <summary>
        /// Отримати AppUserId за ім'ям користувача
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>AppUserId</returns>
        Task<(OperationResult Result, Guid Value, string Message)> GetIdByUserNameAsync(string userName);

        #endregion
        #region # Task<(...)> SetRolesAsync(...)

        /// <summary>
        /// Встановити ролі користувачу
        /// </summary>
        /// <param name="model">Модель додавання ролей</param>
        /// <returns>Ідентифікатор користувача</returns>
        Task<(OperationResult Result, Guid Value, string Message)> SetRolesAsync(SetRolesModel model);

        #endregion
        #region # Task<(...)> SetPasswordAsync(...)

        /// <summary>
        /// Встановити новий пароль користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <param name="pass">Новий пароль користувача</param>
        /// <returns>Ідентиікатор користувача</returns>
        Task<(OperationResult Result, Guid Value, string Message)> SetPasswordAsync(Guid userId, string pass);

        #endregion
    }

    /// <summary>
    /// Репозиторій користувача системи
    /// </summary>
    public class AppUserRepository : IAppUserRepository
    {
        #region # Local variables

        private readonly UserManager<AppUser> _userManager;
        private readonly IAppRoleRepository _appRoleRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserInfo _userInfo;

        #endregion
        #region # IssueRepository constructor

        /// <summary>
        /// AppUserRepository constructor
        /// </summary>
        /// <param name="userManager">Користувач системи</param>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="appRoleRepository">репозиторій роботи з ролями</param>
        public AppUserRepository(ApplicationDbContext dbContext, UserManager<AppUser> userManager, IAppRoleRepository appRoleRepository, IUserInfo userInfo)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _appRoleRepository = appRoleRepository;
            _userInfo = userInfo;
        }

        #endregion

        #region # Task<(...)> GetPagedAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, PagedList<AppUser> Value, string Message)> GetPagedAsync(int pageNumber, int pageSize, string filter, string order)
        {
            try
            {
                var result = await _userManager.Users.AsNoTracking().Paged(pageNumber, pageSize, filter, order);
                return (Result: OperationResult.Ok, Value: result, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
        #region # Task<.....> GetByIdAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, AppUser Value, string Message)> GetByIdAsync(Guid id)
        {
            try
            {
                var dbResult = await _dbContext.Set<AppUser>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                return (Result: OperationResult.Ok, Value: dbResult, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
        #region # Task<.....> GetByNameAsync(...)

        /// <inheritdoc/>
        public async Task<AppUser> GetByNameAsync(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            return result;
        }

        #endregion
        #region # Task<.....> GetUserRoleAsync(...)

        /// <inheritdoc/>
        public async Task<IList<string>> GetUserRoleAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        #endregion
        #region # Task<(...)> GetIdByUserNameAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> GetIdByUserNameAsync(string userName)
        {
            try
            {
                var result = await _userManager.FindByNameAsync(userName);
                return (Result: OperationResult.Ok, Value: result.Id, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(...)> CreateAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> CreateAsync(NewUserModel item)
        {
            try
            {
                var resultRoleName = await _appRoleRepository.GetNameByIdAsync(item.RoleId);
                if (resultRoleName.Result == OperationResult.Error)
                    return (Result: OperationResult.Error, Value: default(Guid), Message: resultRoleName.Message);
                if (string.IsNullOrEmpty(resultRoleName.Value))
                    return (Result: OperationResult.Error, Value: default(Guid), Message: "Помилкова назва ролі.");

                var newUser = new AppUser
                {
                    UserName = item.UserName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CountryId = item.CountryId,
                    FirstName = item.FirstName,
                    LastName = string.IsNullOrEmpty(item.LastName) ? item.UserName : item.LastName,
                    DateOfBirth = item.DateOfBirth,
                    Gender = item.Gender
                };
                var result = await _userManager.CreateAsync(newUser, item.Password);
                if (result.Succeeded)
                {
                    var resultAddToRole = await _userManager.AddToRoleAsync(newUser, resultRoleName.Value);
                    if (resultAddToRole.Succeeded) return (Result: OperationResult.Ok, Value: newUser.Id, Message: "");
                    return (Result: OperationResult.Error, Value: default(Guid), Message: "Помилка додавання ролі.");
                }
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(...)> UpdateAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> UpdateAsync(AppUser item)
        {
            try
            {
                var exist = await _dbContext.Set<AppUser>().FindAsync(item.Id);
                item.NormalizedUserName = item.UserName.ToUpper();
                _dbContext.Entry(exist).CurrentValues.SetValues(item);
                var dbResult = await _dbContext.SaveChangesAsync();
                if (dbResult >= 1) return (Result: OperationResult.Ok, Value: item.Id, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(...)> DeleteAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> DeleteAsync(Guid userId)
        {
            try
            {
                var userToDelete = await _userManager.FindByIdAsync(userId.ToString());
                if (userToDelete == null) return (Result: OperationResult.Error, Value: default(Guid), Message: "Користувача не знайдено.");

                var result = await _userManager.DeleteAsync(userToDelete);
                if (result.Succeeded) return (Result: OperationResult.Ok, Value: userId, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message); }
        }

        #endregion
        #region # Task<(...)> IsExistAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(string userName)
        {
            try
            {
                var result = await _userManager.FindByNameAsync(userName);
                if (result != null) return (Result: OperationResult.Ok, Value: true, Message: "");
                return (Result: OperationResult.Ok, Value: false, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: false, Message: ex.Message); }
        }

        #endregion
        #region # Task<(...)> IsExistAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, bool Value, string Message)> IsExistAsync(Guid userId)
        {
            try
            {
                var result = await _userManager.FindByIdAsync(userId.ToString());
                if (result != null) return (Result: OperationResult.Ok, Value: true, Message: "");
                return (Result: OperationResult.Ok, Value: false, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: false, Message: ex.Message); }
        }

        #endregion
        #region # Task<(...)> SetRolesAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> SetRolesAsync(SetRolesModel model)
        {
            SetRolesModel result = new SetRolesModel { AppUserId = model.AppUserId };
            try
            {
                var user = await _userManager.FindByIdAsync(model.AppUserId.ToString());
                List<string> roles = new List<string>();

                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);

                //// получаем все роли
                //var allRoles = _roleManager.Roles.ToList();

                foreach (var x in model.RoleIds)
                {
                    var role = await _appRoleRepository.GetByIdAsync(x);
                    if (role.Result == OperationResult.Ok)
                    {
                        roles.Add(role.Value.Name);
                    }
                }

                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return (Result: OperationResult.Ok, Value: model.AppUserId, Message: "");
            }
            catch (Exception ex)
            {
                return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message);
            }
        }

        #endregion
        #region # Task<(...)> SetPasswordAsync(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> SetPasswordAsync(Guid userId, string pass)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var changeResult = await _userManager.ResetPasswordAsync(user, token, pass);
                if (changeResult.Succeeded)
                    return (Result: OperationResult.Ok, Value: userId, Message: "");
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Щось пішло не за планом...");
            }
            catch (Exception ex)
            {
                return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message);
            }
        }

        #endregion
    }
}