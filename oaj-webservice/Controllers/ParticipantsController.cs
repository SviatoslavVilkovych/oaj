using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OAJ.WebService.Internal.Communication;
using System;

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
        /// <param name="taskName">Name of the task.</param>
        /// <param name="language">Language in which program written.</param>
        /// <param name="program">Code of the program.</param>
        /// <returns>Id of the process and location where result can be found.</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult Send([FromRoute] string taskName, [FromQuery] string language, [FromBody] string program)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get program result.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <param name="id">Id of the verification.</param>
        /// <returns>Formatted result of time of execution, passed tests, etc.</returns>
        [HttpGet]
        [Route("[action]/{taskName}/{id}")]
        public IActionResult Get([FromRoute] string taskName, [FromRoute] string id)
        {
            throw new NotImplementedException();
        }

        private readonly ILogger<ParticipantsController> _logger;
        private readonly IJudgeService JudgeServiceInstance;
    }
}
