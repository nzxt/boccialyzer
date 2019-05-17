using System;
using Boccialyzer.Core.Repository;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boccialyzer.Web.Controllers
{
    /// <summary>
    /// Статистика
    /// </summary>
    [Produces("application/json")]
    [Route("api/Stat")]
    [Authorize]
    [ApiController]
    public class StatController : ControllerBase
    {
        #region # Local variables

        private readonly IStatRepository _statRepository;

        #endregion
        #region # StatController constructor

        /// <summary>
        /// StatController constructor
        /// </summary>
        /// <param name="statRepository"></param>
        public StatController(IStatRepository statRepository)
        {
            _statRepository = statRepository;
        }

        #endregion

        #region # Get - Отримати статистику

        /// <summary>
        /// Отримати статистику
        /// </summary>
        /// <param name="param">Параметри статистики</param>
        /// <returns>Статистика</returns>
        /// <response code="200">Успішне виконання</response>
        /// <response code="422">Помилка виконання</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] StatParam param)
        {
            if (param == null) return StatusCode(422, "Відсутні параметри.");
            var result = await _statRepository.GetStatistic(param);
            if (result.Result == OperationResult.Ok) return StatusCode(200, result.Value);
            return StatusCode(422, result.Message);
        }

        #endregion
        //#region # Get(string id) - Отримати статистику тренування

        ///// <summary>
        ///// Отримати статистику тренування
        ///// </summary>
        ///// <param name="id">Ідентифікатор тренування</param>
        ///// <returns>Деталі запису</returns>
        ///// <response code="200">Успішне виконання</response>
        ///// <response code="422">Помилка виконання</response>
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    if (string.IsNullOrEmpty(id)) return StatusCode(422, "Відсутній ідентифікатор.");
        //    Guid itemId;
        //    try { itemId = Guid.Parse(id); }
        //    catch (Exception ex) { return StatusCode(422, $"Помилковий ідентифікатор. {ex.Message}"); }

        //    var result = await _statRepository.GetTrainingStat(itemId);
        //    if (result.Result == OperationResult.Ok) return StatusCode(200, result.Value);
        //    return StatusCode(422, result.Message);
        //}

        //#endregion
    }
}