using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using System;
using System.Threading.Tasks;
using Boccialyzer.Domain.Dtos;

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

        #endregion
        #region # AccountRepository - конструктор

        public AccountRepository(ApplicationDbContext dbContext, IAppUserRepository appUserRepository, IUserInfo userInfo)
        {
            _dbContext = dbContext;
            _appUserRepository = appUserRepository;
            _userInfo = userInfo;
        }

        #endregion


        #region # Task<(...)> Create(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, Guid Value, string Message)> Create(NewUserModel user)
        {
            try
            {



                //string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(code));
                //var invitationId = decodedString.ToGuid();
                //if (!invitationId.Equals(default(Guid)))
                //{
                //    var result = await _dbContext.Invitations.FindAsync(invitationId);
                //    if (result != null)
                //        return (Result: OperationResult.Ok, Value: result.Id, Message: "");
                //}
                return (Result: OperationResult.Error, Value: default(Guid), Message: "Запрошення не знайдено.");
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
