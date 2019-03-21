using Boccialyzer.Core.Repository;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Boccialyzer.Web.Controllers
{
    /// <summary>
    /// Керування ролями (ONLY FOR ROLE ADMINISTRATOR)
    /// </summary>
    [Produces("application/json")]
    [Route("api/AppRole")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class AppRoleController : Controller
    {
        #region # Local variables

        private readonly IAppRoleRepository _appRoleRepository;

        #endregion
        #region # AppRoleController constructor

        /// <summary>
        /// AppRoleController constructor
        /// </summary>
        /// <param name="appRoleRepository">Репозиторій роботи з ролями системи</param>
        public AppRoleController(IAppRoleRepository appRoleRepository)
        {
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
            var result = await _appRoleRepository.GetPagedAsync(pageNumber, pageSize, filter, order);
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
            try { Guid itemId = Guid.Parse(id); }
            catch (Exception ex) { return StatusCode(422, string.Format("Помилковий ідентифікатор. {0}", ex.Message)); }

            var result = await _appRoleRepository.GetByIdAsync(id);
            if (result.Result == OperationResult.Ok) return StatusCode(200, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
        #region # Post - Створити новий запис

        /// <summary>
        /// Створити новий запис
        /// </summary>
        /// <param name="item">Новий об'єкт</param>
        /// <returns>Ідентифікатор створеного запису</returns>
        /// <response code="201">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppRole item)
        {
            if (item == null) return StatusCode(422, "Відсутні данні.");
            if (string.IsNullOrEmpty(item.Name)) return StatusCode(422, "Відсутня назва ролі.");

            var result = await _appRoleRepository.CreateAsync(item);
            if (result.Result == OperationResult.Ok) return StatusCode(201, result.Value);
            return StatusCode(422, "Відсутні данні.");
        }

        #endregion
        #region # Put - Модифікувати запис

        /// <summary>
        /// Модифікувати запис
        /// </summary>
        /// <param name="item">Об'єкт</param>
        /// <returns>Ідентифікатор запису</returns>
        /// <response code="202">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [ProducesResponseType(202)]
        [ProducesResponseType(422)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]AppRole item)
        {
            if (item == null) return StatusCode(422, "Відсутні данні.");
            var result = await _appRoleRepository.UpdateAsync(item);
            if (result.Result == OperationResult.Ok) return StatusCode(202, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
        #region # GetDefault() - Отримати ідентифікатор ролі за замовчуванням

        /// <summary>
        /// Отримати ідентифікатор ролі за замовчуванням
        /// </summary>
        /// <returns>Ідентифікатор ролі за замовчуванням</returns>
        /// <response code="200">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [HttpGet("GetDefault")]
        public async Task<IActionResult> GetDefault()
        {
            var result = await _appRoleRepository.GetDefaultIdAsync();
            if (result.Result == OperationResult.Ok) return StatusCode(200, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
    }
}