using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OAJ.WebService.Internal.Communication;
using OAJ.WebService.Models;
using System;
using System.Threading.Tasks;

namespace OAJ.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantsController : ControllerBase
    {

        public ParticipantsController(IJudgeService judgeService, ILogger<ParticipantsController> logger)
        {
            _logger = logger;
            JudgeServiceInstance = judgeService;
        }

        /// <summary>
        /// Send program to verification (this request is fire-and-forget).
        /// </summary>
        /// <param name="program">Code of the program.</param>
        /// <returns>Id of the process and location where result can be found.</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Send([FromBody] TaskCheck program)
        {
            return Accepted(await JudgeServiceInstance.CheckTaskAsync(program));
        }

        /// <summary>
        /// Get program result.
        /// </summary>
        /// <param name="id">Id of the verification.</param>
        /// <returns>Formatted result of time of execution, passed tests, etc.</returns>
        [HttpGet]
        [Route("[action]/{taskName}/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await JudgeServiceInstance.CheckTaskAsync1(id));
        }

        private readonly ILogger<ParticipantsController> _logger;
        private readonly IJudgeService JudgeServiceInstance;
    }
}
