using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Enums;
using System;
using System.Text;
using System.Threading.Tasks;
using Boccialyzer.Domain.Models;

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
        //#region # Task<UserProfile> GetUserProfile(string userName)

        ///// <summary>
        ///// Отримати профіль користувача
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //Task<(OperationResult Result, UserProfile Value, string Message)> GetUserProfile(string userName);

        //#endregion
    }

    /// <summary>
    /// Репозиторій аккаунту
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        #region # Local variables


        private readonly ApplicationDbContext _dbContext;
        private readonly IAppUserRepository _appUserRepository;

        #endregion
        #region # AccountRepository - конструктор

        public AccountRepository(ApplicationDbContext dbContext, IAppUserRepository appUserRepository)
        {
            _dbContext = dbContext;
            _appUserRepository = appUserRepository;
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


        //#region # Task<UserProfile> GetUserProfile(string userName)

        ///// <inheritdoc/>
        //public async Task<(OperationResult Result, UserProfile Value, string Message)> GetUserProfile(string userName)
        //{
        //    try
        //    {
        //        var userProfile = new UserProfile
        //        {
        //            UserName = userName,
        //            Roles = await _appUserRepository.GetUserRoleAsync(userName)
        //        };
        //        var profile = await _appUserRepository.GetPerson(userName);
        //        if (profile.Result == OperationResult.Error)
        //            return (Result: OperationResult.Error, Value: null, Message: profile.Message);

        //        userProfile.AppUserId = profile.Value.AppUser.Id;
        //        userProfile.PersonId = profile.Value.Id;

        //        userProfile.WorkPlace = _dbContext.WorkPlace.FirstOrDefault(w => w.PersonToWorkPlaces.FirstOrDefault(a => a.IsDefault == true).PersonId == userProfile.PersonId);

        //        return (Result: OperationResult.Ok, Value: userProfile, Message: "");
        //    }
        //    catch (Exception ex)
        //    { return (Result: OperationResult.Error, Value: null, Message: ex.Message); }
        //}

        //#endregion
    }
}
