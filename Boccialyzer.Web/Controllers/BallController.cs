using System;
using System.Threading.Tasks;
using Boccialyzer.Core.Repository;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boccialyzer.Web.Controllers
{
    /// <summary>
    /// М'яч
    /// </summary>
    [Produces("application/json")]
    [Route("api/Ball")]
    [Authorize]
    [ApiController]
    public class BallController : Controller
    {
        #region # Local variables

        private readonly IBallRepository _ballRepository;

        #endregion
        #region # Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ballRepository"></param>
        public BallController(IBallRepository ballRepository)
        {
            _ballRepository = ballRepository;
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
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 25, string filter = "", string order = "")
        {
            var result = await _ballRepository.GetPaged(pageNumber, pageSize, filter, order);
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
            catch (Exception ex) { return StatusCode(422, $"Помилковий ідентифікатор. {ex.Message}"); }

            var result = await _ballRepository.GetByIdAsync(itemId);
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
        public async Task<IActionResult> Post([FromBody] Ball item)
        {
            if (item == null) return StatusCode(422, "Відсутні данні.");
            //if (string.IsNullOrEmpty(item.Name)) return StatusCode(422, "Відсутня назва.");

            var result = await _ballRepository.CreateAsync(item);
            if (result.Result == OperationResult.Ok) return StatusCode(201, result.Value);
            return StatusCode(422, result.Message);
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
        public async Task<IActionResult> Put([FromBody] Ball item)
        {
            if (item == null) return StatusCode(422, "Відсутні данні.");
            var result = await _ballRepository.UpdateAsync(item);
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
        //[Authorize(Roles = "SystemAdministrator, Head")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return StatusCode(422, "Відсутній ідентифікатор.");
            Guid itemId;
            try { itemId = Guid.Parse(id); }
            catch (Exception ex) { return StatusCode(422, $"Помилковий ідентифікатор. {ex.Message}"); }

            var result = await _ballRepository.DeleteAsync(itemId);
            if (result.Result == OperationResult.Ok) return StatusCode(204, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
    }
}