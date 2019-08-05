using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blyzer.Domain.Models;
using Blyzer.Domain.Models.Fsp;
using Blyzer.Repository.Repository;

namespace Blyzer.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValueRepository _valueRepository;
        public ValuesController(IValueRepository valueRepository)
        {
            _valueRepository = valueRepository;
        }


        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(int page, int pageSize, string filters = "", string sorts = "")
        {
            var result = Request.QueryString.HasValue
                ? await _valueRepository.GetAsync(new GetParametersModel(page: page, pageSize: pageSize, filters: filters, sorts: sorts))
                : await _valueRepository.GetAsync();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
