using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Dtos;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using System;
using System.Threading.Tasks;
using Boccialyzer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій аккаунту
    /// </summary>
    public interface IAccountRepository
    {
        #region # Task<(...)> Create(...)

        /// <summary>
        /// Створити новий аккаунт
        /// </summary>
        /// <param name="user">Модель для сворення нового користувача (респондента)</param>
        /// <returns>Ідентифікатор нового користувача</returns>
        Task<(OperationResult Result, Guid Value, string Message)> Create(NewUserModel user);

        #endregion
        #region # (...) GetUserProfile()

        /// <summary>
        /// Отримати профіль користувача
        /// </summary>
        /// <returns></returns>
        (OperationResult Result, UserProfile Value, string Message) GetUserProfile();

        #endregion
    }

    /// <summary>
    /// Репозиторій аккаунту
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        #region # Local variables


        private readonly ApplicationDbContext _dbContext;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUserInfo _userInfo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppRoleRepository _appRoleRepository;

        #endregion
        #region # AccountRepository - конструктор

        public AccountRepository(ApplicationDbContext dbContext, IAppUserRepository appUserRepository, IUserInfo userInfo, UserManager<AppUser> userManager, IAppRoleRepository appRoleRepository)
        {
            _dbContext = dbContext;
            _appUserRepository = appUserRepository;
            _userInfo = userInfo;
            _userManager = userManager;
            _appRoleRepository = appRoleRepository;
        }

        #endregion


        #region # Task<(...)> Create(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> Create(NewUserModel user)
        {
            try
            {
                var newUser = new AppUser
                {
                    UserName = user.UserName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CountryId = user.CountryId,
                    FirstName = user.FirstName,
                    LastName = string.IsNullOrEmpty(user.LastName) ? user.UserName : user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender
                };
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    var resultDefaultId = await _appRoleRepository.GetDefaultAsync();
                    if (resultDefaultId.Result == OperationResult.Ok)
                    {
                        var resultAddToRole = await _userManager.AddToRoleAsync(newUser, resultDefaultId.Value.Name);
                        if (resultAddToRole.Succeeded)
                            return (Result: OperationResult.Ok, Value: newUser.Id, Message: "");
                    }

                    return (Result: OperationResult.Error, Value: default(Guid), Message: "Помилка додавання ролі.");
                }
                return (Result: OperationResult.Error, Value: default(Guid), Message: JsonConvert.SerializeObject(result.Errors));
            }
            catch (Exception ex)
            {
                return (Result: OperationResult.Error, Value: default(Guid), Message: ex.Message);
            }
        }

        #endregion

        #region # (...) GetUserProfile()

        /// <inheritdoc/>
        public (OperationResult Result, UserProfile Value, string Message) GetUserProfile()
        {
            try
            {
                var userProfile = new UserProfile
                {
                    UserName = _userInfo.UserName,
                    Roles = _userInfo.Roles,
                    AppUserId = _userInfo.AppUserId
                };

                return (Result: OperationResult.Ok, Value: userProfile, Message: "");
            }
            catch (Exception ex)
            { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        }

        #endregion
    }
}
