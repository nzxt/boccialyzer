using Boccialyzer.Core.Repository;
using Boccialyzer.Domain.Dtos;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace Boccialyzer.Web.Controllers
{
    /// <summary>
    /// Робота з аккаунтом
    /// </summary>
    [Produces("application/json")]
    [Route("api/Account")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region # Local variables
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogRepository _log;

        #endregion
        #region # AccountController - конструктор

        /// <summary>
        /// AccountController конструктор
        /// </summary>
        /// <param name="signInManager">Репозиторій роботи з авторизацією</param>
        /// <param name="accountRepository">Репозиторій роботи з аккаунтом</param>
        /// <param name="logRepository">Репозиторій логування</param>
        public AccountController(SignInManager<AppUser> signInManager, IAccountRepository accountRepository, ILogRepository logRepository)
        {
            _signInManager = signInManager;
            _accountRepository = accountRepository;
            _log = logRepository;
        }

        #endregion
        #region # Login - Авторизація користувача

        /// <summary>
        /// Авторизація користувача
        /// </summary>
        /// <param name="loginModel">Модель авторизації</param>
        /// <response code="200">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            if (loginModel == null) return StatusCode(422, "Відсутні данні.");
            if (string.IsNullOrEmpty(loginModel.UserName)) return StatusCode(422, "Відсутнє ім'я користувача.");
            if (string.IsNullOrEmpty(loginModel.Password)) return StatusCode(422, "Відсутній пароль");
            var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                Log.Information("{ControllerInfo}", "Успішна авторизація.");
                return Ok();
            }
            Log.Error("{ControllerError}", "Помилка авторизації.");

            return StatusCode(422, "Помилка авторизації.");
        }

        #endregion
        #region # Logout - Вихід з системи

        /// <summary>
        /// Вихід з системи
        /// </summary>
        /// <returns>Вітання</returns>
        /// <response code="200">Успішне виконання</response>
        [Authorize]
        [HttpGet]
        [Route("Logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Фух... Робота завершена! :-)");
        }

        #endregion
        #region # GetProfile - Отримати профіль користувача

        /// <summary>
        /// Отримати профіль користувача
        /// </summary>
        /// <returns>Профіль користувача</returns>
        /// <response code="200">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [Authorize]
        [HttpGet]
        [Route("GetProfile")]
        [ProducesResponseType(200)]
        [HttpGet]
        public IActionResult GetProfile()
        {
            var result = _accountRepository.GetUserProfile();
            if (result.Result == OperationResult.Ok) return StatusCode(200, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion

        #region # Registration - реєстрація нового користувача

        /// <summary>
        /// Реєстрація нового користувача
        /// </summary>
        /// <param name="item">Модель реєстрації</param>
        /// <returns>Профіль користувача</returns>
        /// <response code="200">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("Registration")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> Registration([FromBody] NewUserModel item)
        {
            if (!ModelState.IsValid) return StatusCode(422, "Помилкові данні.");
            if (string.IsNullOrEmpty(item.UserName)) return StatusCode(422, "Відсутнє ім'я користувача.");
            if (string.IsNullOrEmpty(item.Password)) return StatusCode(422, "Відсутній пароль.");
            var result = await _accountRepository.Create(item);
            if (result.Result == OperationResult.Ok) return StatusCode(201, result.Value);

            return StatusCode(422, result.Message);
        }

        #endregion
    }
}