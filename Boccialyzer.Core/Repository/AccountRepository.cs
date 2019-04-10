using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Dtos;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boccialyzer.Core.Repository
{
    /// <summary>
    /// Репозиторій аккаунту
    /// </summary>
    public interface IAccountRepository
    {
        #region # Task<(...)> LogIn(...)

        /// <summary>
        /// Авторизація користувача
        /// </summary>
        /// <param name="login">Модель авторизації</param>
        /// <returns>Токен</returns>
        Task<(OperationResult Result, string Value, string Message)> LogIn(LoginDto login);

        #endregion
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
        private readonly AppAuthOption _appAuthOption;

        #endregion
        #region # AccountRepository - конструктор

        public AccountRepository(ApplicationDbContext dbContext, IAppUserRepository appUserRepository, IUserInfo userInfo, UserManager<AppUser> userManager, IAppRoleRepository appRoleRepository, IOptionsSnapshot<AppAuthOption> appAuthOption)
        {
            _dbContext = dbContext;
            _appUserRepository = appUserRepository;
            _userInfo = userInfo;
            _userManager = userManager;
            _appRoleRepository = appRoleRepository;
            _appAuthOption = appAuthOption.Value;
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
        #region # Task<(...)> LogIn(...)

        /// <inheritdoc/>
        public async Task<(OperationResult Result, string Value, string Message)> LogIn(LoginDto login)
        {
            try
            {
                var user = await _userManager.Users.Where(x => x.UserName == login.UserName).FirstOrDefaultAsync();
                if (user != null)
                {
                    var isValidPassword = await _userManager.CheckPasswordAsync(user, login.Password);
                    if (isValidPassword)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        var isAdmin = await _userManager.IsInRoleAsync(user, Role.Admin);

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = _appAuthOption.GetSymmetricSecurityKey();
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim("id", user.Id.ToString()),
                            new Claim("is_admin", isAdmin.ToString()),
                            new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(_appAuthOption.Expiration),
                            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                            Audience = _appAuthOption.Audience,
                            IssuedAt = DateTime.UtcNow,
                            Issuer = _appAuthOption.Issuer,
                            NotBefore = DateTime.UtcNow
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenString = tokenHandler.WriteToken(token);

                        return (Result: OperationResult.Ok, Value: tokenString, Message: "");
                    }
                }

                return (Result: OperationResult.Error, Value: string.Empty, Message: "Помилка авторизації.");
            }
            catch (Exception ex)
            {
                return (Result: OperationResult.Error, Value: string.Empty, Message: ex.Message);
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
                    Role = _userInfo.Role,
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
