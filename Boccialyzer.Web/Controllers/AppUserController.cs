﻿using Boccialyzer.Core.Repository;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Boccialyzer.Web.Controllers
{
    /// <summary>
    /// AppUser Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/AppUser")]
    [Authorize]
    [ApiController]
    public class AppUserController : Controller
    {
        #region # Local variables

        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppRoleRepository _appRoleRepository;

        #endregion
        #region # AppUserController constructor

        /// <summary>
        /// AppUserController constructor
        /// </summary>
        /// <param name="appUserRepository">Репозиторій роботи з користувачами системи</param>
        /// <param name="appRoleRepository">Репозиторій роботи з ролями системи</param>
        public AppUserController(IAppUserRepository appUserRepository, IAppRoleRepository appRoleRepository)
        {
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
        }

        #endregion

        #region # Get - Отримати список (пагінація)

        /// <summary>
        /// Отримати список (пагінація)
        /// </summary>
        /// <param name="pageNumber">Номер сторінки (за замовчуванням 1)</param>
        /// <param name="pageSize">Кількість записів на сторінку (за замовчуванням 25)</param>
        /// <param name="filter">Фільтр</param>
        /// <param name="order">Сортування</param>
        /// <returns>Список з пагінацією</returns>
        /// <response code="200">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [HttpGet]
        public async Task<ActionResult> Get(int pageNumber = 1, int pageSize = 25, string filter = "", string order = "")
        {
            var result = await _appUserRepository.GetPagedAsync(pageNumber, pageSize, filter, order);
            if (result.Result == OperationResult.Ok) return StatusCode(200, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
        #region # Get(string id) - Отримати деталі

        /// <summary>
        /// Отримати деталі за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор запису</param>
        /// <returns>Деталі запису</returns>
        /// <response code="200">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return StatusCode(422, "Відсутній ідентифікатор.");
            Guid itemId;
            try { itemId = Guid.Parse(id); }
            catch (Exception ex) { return StatusCode(422, string.Format("Помилковий ідентифікатор. {0}", ex.Message)); }

            var result = await _appUserRepository.GetByIdAsync(itemId);
            if (result.Result == OperationResult.Ok) return StatusCode(200, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
        //#region # Post - Створити новий запис

        ///// <summary>
        ///// Створити новий запис
        ///// </summary>
        ///// <param name="userName">Ім'я користувача</param>
        ///// <param name="password">Пароль (опціонально)</param>
        ///// <param name="personId">Ідентифікатор персони</param>
        ///// <param name="roleId">Ідентифікатор ролі (опціонально)</param>
        ///// <returns>Ідентифікатор створеного запису</returns>
        ///// <response code="201">Успішне виконання</response>
        ///// <response code="422">Помилка виконання</response>
        //[ProducesResponseType(201)]
        //[ProducesResponseType(422)]
        //[HttpPost]
        //public async Task<IActionResult> Post(string userName, string personId, string password = "", string roleId = "")
        //{
        //    if (string.IsNullOrEmpty(userName)) return StatusCode(422, "Відсутнє ім'я користувача.");
        //    var userExist = await _appUserRepository.IsExistAsync(userName);
        //    if (userExist.Result == OperationResult.Error) return StatusCode(422, userExist.Message);
        //    if (userExist.Value) return StatusCode(422, "Користувач вже існує.");

        //    //if (string.IsNullOrEmpty(password)) return StatusCode(422, "Відсутній пароль.");
        //    //if (password.Length < 3) return StatusCode(422, "Пароль занадто короткий.");

        //    if (string.IsNullOrEmpty(personId)) return StatusCode(422, "Відсутній ідентифікатор особи.");
        //    Guid personGuid;
        //    try { personGuid = Guid.Parse(personId); }
        //    catch (Exception ex) { return StatusCode(422, string.Format("Помилковий ідентифікатор. {0}", ex.Message)); }

        //    var personExist = await _personRepository.IsExistAsync(personGuid);
        //    if (personExist.Result == OperationResult.Error) return StatusCode(422, personExist.Message);
        //    if (!personExist.Value) return StatusCode(422, "Особа не існує.");

        //    Guid roleGuid;
        //    if (string.IsNullOrEmpty(roleId))
        //    {
        //        var resultDefaultId = await _appRoleRepository.GetDefaultIdAsync();
        //        if (resultDefaultId.Result == OperationResult.Ok)
        //        {
        //            if (resultDefaultId.Value == default(Guid)) return StatusCode(422, "Роль за замовчуванням не знайдена.");
        //            roleGuid = resultDefaultId.Value;
        //        }
        //        else
        //            return StatusCode(422, resultDefaultId.Message);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            roleGuid = Guid.Parse(roleId);
        //            var isRoleExist = await _appRoleRepository.IsExistAsync(roleGuid);
        //            if (isRoleExist.Result == OperationResult.Error) return StatusCode(422, userExist.Message);
        //            if (!isRoleExist.Value) return StatusCode(422, "Роль не існує.");
        //        }
        //        catch (Exception ex) { return StatusCode(422, string.Format("Помилковий ідентифікатор ролі. {0}", ex.Message)); }
        //    }
        //    var result = await _appUserRepository.CreateAsync(userName, password, personGuid, roleGuid);
        //    if (result.Result == OperationResult.Ok) return StatusCode(201, result.Value);
        //    return StatusCode(422, result.Message);
        //}

        //#endregion
        #region # Put - Модифікувати запис

        /// <summary>
        /// Модифікувати користувача
        /// </summary>
        /// <param name="item">Користувач</param>
        /// <returns>Ідентифікатор користувача</returns>
        /// <response code="202">Запит завершився вдало</response>
        /// <response code="422">Сталась помилка при обробці запиту</response>
        [ProducesResponseType(202)]
        [ProducesResponseType(422)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AppUser item)
        {
            if (item == null) return StatusCode(422, "Відсутні данні.");
            //if (item.PersonId == default(Guid)) return StatusCode(422, "Відсутній ідентифікатор персони.");
            var result = await _appUserRepository.UpdateAsync(item);
            if (result.Result == OperationResult.Ok) return StatusCode(202, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
        #region # Delete(string id) - Видалити запис

        /// <summary>
        /// Видалити запис
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <returns>Ідентифікатор запису</returns>
        /// <response code="204">Успішне завершення</response>
        /// <response code="422">Помилка додавання</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return StatusCode(422, "Відсутній ідентифікатор.");
            Guid itemId;
            try { itemId = Guid.Parse(id); }
            catch (Exception ex) { return StatusCode(422, string.Format("Помилковий ідентифікатор. {0}", ex.Message)); }

            var result = await _appUserRepository.DeleteAsync(itemId);
            if (result.Result == OperationResult.Ok) return StatusCode(204, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
        //#region # SetRoles(SetRolesModel rolesModel)

        ///// <summary>
        ///// Призначити ролі користувачу
        ///// </summary>
        ///// <param name="rolesModel">Модель для додавання ролей користувачу</param>
        ///// <returns>Ідентифікатор користувача</returns>
        ///// <response code="200">Успішне виконання</response>
        ///// <response code="422">Помилка виконання</response>
        //[ProducesResponseType(200)]
        //[ProducesResponseType(422)]
        //[HttpPost("SetRoles")]
        //public async Task<IActionResult> SetRoles([FromBody] SetRolesModel rolesModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        if (rolesModel == null) return StatusCode(422, "Відсутні данні.");
        //        if (rolesModel.AppUserId == default(Guid)) return StatusCode(422, "Відсутній ідентифікатор користувача.");
        //        if (!rolesModel.RoleIds.Any()) return StatusCode(422, "Відсутні ідентифікатори ролей.");
        //        return StatusCode(422, ModelState);
        //    }

        //    var userExist = await _appUserRepository.IsExistAsync(rolesModel.AppUserId);
        //    if (userExist.Result == OperationResult.Error) return StatusCode(422, userExist.Message);
        //    if (!userExist.Value) return StatusCode(422, "Користувач не існує.");

        //    var result = await _appUserRepository.SetRoles(rolesModel);
        //    if (result.Result == OperationResult.Ok) return StatusCode(200, result.Value);
        //    return StatusCode(422, result.Message);
        //}

        //#endregion
        //#region # SetPass(Guid userId, string pass)

        ///// <summary>
        ///// Встановити новий пароль
        ///// </summary>
        ///// <param name="userId">Ідентифікатор користувача</param>
        ///// <param name="pass">Новий пароль</param>
        ///// <returns>Ідентифікатор користувача</returns>
        ///// <response code="202">Успішне виконання</response>
        ///// <response code="422">Помилка виконання</response>
        //[ProducesResponseType(202)]
        //[ProducesResponseType(422)]
        //[HttpPost("SetPass")]
        //public async Task<IActionResult> SetPass(Guid userId, string pass)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        if (userId == default(Guid)) return StatusCode(422, "Відсутній ідентифікатор користувача.");
        //        if (string.IsNullOrEmpty(pass)) return StatusCode(422, "Відсутній пароль користувача.");
        //        return StatusCode(422, ModelState);
        //    }
        //    var userExist = await _appUserRepository.IsExistAsync(userId);
        //    if (userExist.Result == OperationResult.Error) return StatusCode(422, userExist.Message);
        //    if (!userExist.Value) return StatusCode(422, "Користувач не існує.");
        //    var result = await _appUserRepository.SetPassword(userId, pass);
        //    if (result.Result == OperationResult.Ok) return StatusCode(202, result.Value);
        //    return StatusCode(422, result.Message);
        //}

        //#endregion
    }
}